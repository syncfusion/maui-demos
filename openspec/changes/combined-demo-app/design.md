# Design

## Context

See proposal.md for why this exists. Requirements are in `specs/combined-sample-browser/spec.md`.

There are 67 sample executables under `MAUI/<Control>/`. Each calls `BaseConfig.MainPageInit` with its own assembly and sets `BaseConfig.IsIndividualSB` to `true`. `SampleBrowser.Maui.Base` 35.1.37 already supplies the catalog UI (`MainPageAndroid`, `MainPageiOS`, `MainPageMacCatalyst`, `MainPageWindows`): a navigation drawer, search, categories, `NavigationPage`, and `CallOnDisappearing`. `BaseConfig.AvailableControlStreamList` is the public field a combined host is meant to fill. No project in this repo sets it.

There is no `Directory.Build.props` today. MSBuild will import one placed at the repository root for every project under it, including the per-control solutions. Sample platform folders mix entry points (`MainActivity`, `AppDelegate`, `Program`, Windows `App.xaml`) with sample helpers (`SaveAndroid.cs`, `PrintService.cs`, and similar).

## Goals / Non-Goals

**Goals:**

- Host every sample behind the existing Base catalog, with one sample alive at a time.
- Add only new files, plus a README preamble, so upstream sample projects merge cleanly.
- Keep per-control solutions as standalone executables.
- Make the fork safe to publish: no license key, Apple certificate, provisioning profile, or private key in git.

**Non-Goals:**

- AppShell, a custom flyout, or prefetching the next sample.
- 1Password or any other secret manager.
- Editing sample `.csproj` files or sample source. iOS native-name clashes are rewritten into `obj/` during the combined build only.
- Committing the owner's Apple team id, certificate, or provisioning profile.

## Decisions

### Use the Base catalog, with a navigator fallback

The host sets `IsIndividualSB` to `true`, loads each sample assembly's `ControlList.xml` and `*SamplesList.xml` into `AvailableControlStreamList`, registers the license once, and uses the page returned by `MainPageInit` as the window page.

The first implementation task is a spike on that field. If the stock page lists every control and pops the previous sample, the host keeps that page and, on pop, disconnects the sample handler if the stock page has not already released native resources.

If the stock page cannot take every assembly, or it retains visited samples, the host uses its own `NavigationPage` and a grouped `CollectionView` fed by the same XML. That menu is not a Shell flyout. Selection creates the sample type from the owning assembly. Pop calls `OnDisappearing`, disconnects the handler, and clears the page content before the next sample is created. The busy indicator already present on the Base page, or an equivalent on the fallback page, covers the UI-thread hitch while a sample is constructed.

Alternatives considered: an AppShell item per control keeps each visited page for the life of the Shell, which fails the one-live-sample requirement. A `CollectionView` flyout as the primary design rebuilds search, categories, and per-platform layouts the Base package already has, so it is only the fallback.

### Combined build is selected by solution name

New files:

- `SampleBrowser.Maui.Combined.sln` at the repository root
- `MAUI/Combined/SampleBrowser.Maui.Combined/` as the only executable in that solution
- `Directory.Build.props` and `Directory.Build.targets` at the repository root

`Directory.Build.props` sets `CombinedSampleBrowser` only when `$(SolutionName)` is `SampleBrowser.Maui.Combined` or the property is already true. Per-control solutions have other solution names, so their projects stay `OutputType` `Exe`.

When `CombinedSampleBrowser` is true and the project is not the host, `Directory.Build.targets` sets `OutputType` to `Library` and removes platform startup files (`MainActivity`, `MainApplication`, `AppDelegate`, `Program`, Tizen `Main.cs`, Windows `App.xaml`). Sample `App.xaml` stays, because sample code refers to that `App` type. `MauiProgram.cs` is removed so the library does not declare a second app builder. Save, print, and preview helpers under `Platforms/` stay.

The host project references every sample `.csproj`. Those references live only in the new host project. The root solution lists the host and the sample projects so one solution build passes `SolutionName` into the referenced projects. .NET Meteor does not build the solution. Its status-bar launch builds the selected `.csproj` directly, so `SolutionName` is empty unless the launch task also passes `-p:CombinedSampleBrowser=true`. That property is global and reaches the referenced sample projects. Do not hardcode the target framework or runtime identifier in that task: Meteor fills both from the status bar, then applies `args`.

Alternative considered: editing each sample project to be a library all the time. That makes every upstream merge touch those projects and stops the per-control solutions from producing apps.

### Partial builds

Host edits do not require recompiling the samples. Build the host project with `-p:CombinedSampleBrowser=true -p:BuildProjectReferences=false`. MSBuild then packages the sample libraries already in `bin` for that configuration and target framework. Rebuild one sample, with `CombinedSampleBrowser` set, only when that sample's sources changed, then build the host the same way. Rebuild the solution when `Directory.Build.props` or `Directory.Build.targets` change, because those files change how every sample compiles. Commands are in the README section "Building the combined app" and in `AGENTS.md`.

### Combined-build fixes

These look like unused cleanup. Each one is a build failure that appears only when the samples are libraries inside the host. Do not delete them, and do not push the workaround into the upstream sample files.

- **NuGet audit.** DataGrid, DocIO, PDF, PdfViewer, Presentation, and XlsIO set `TreatWarningsAsErrors`. Their document packages pull in `System.Security.Cryptography.Pkcs` and `System.Security.Cryptography.Xml` 10.0.11, which NuGet reports as `NU1902` and `NU1903`. `NuGetAudit` is set false only while `CombinedSampleBrowser` is true. Standalone solutions keep their own audit policy.
- **Keep sample `App.xaml`.** Backdrop, Calendar, and AIAssistView call `App.Current` and read resources from that `App` type. Removing `App.xaml` / `App.xaml.cs` does not compile. Remove `MauiProgram.cs` and the platform entry points (`MainActivity`, `AppDelegate`, `Program`, and so on) so the samples are not second applications. Removing `AppDelegate.cs` also avoids dozens of `[Register("AppDelegate")]` collisions.
- **Icons and splash screens.** The resizetizer requires unique image output names. Every sample contributes `appicon` and a splash screen, and many contribute `dotnet_bot`. Libraries drop `MauiIcon` and `MauiSplashScreen` so the host icon stays the combined app's mark. Sample `AndroidManifest.xml` files still reference `@mipmap/appicon`, so a combined Android library build copies the manifest into `obj/` and removes `android:icon` and `android:roundIcon` before aapt runs. The upstream manifest is not edited. Release would otherwise compile sample XAML with XamlC, which cannot resolve `LegendItem` in the Funnel, Polar, and Pyramid chart samples. Those three use the runtime XAML inflator in combined builds. SourceGen compiles that XAML, then fails nullable event-handler warnings that these projects treat as errors. Sample projects stay executables in their own project files, so the SDK still generates `runtimeconfig.json`. Combined builds turn that off; the file is not part of the host app, and the folder it would be written to is not created for a library. Android library output stays in the target-framework directory. The app's per-architecture build then reenters each sample with the compiler skipped and links the DLL from an `android-arm64` subdirectory of both `obj` and `bin`, so the combined build copies the already compiled DLL and PDB into those two subdirectories. The host sets `ResizetizerErrorOnDuplicateOutputFilename` to false so the remaining duplicate `MauiImage` files do not fail the build. Do not remove `MauiImage`; samples still need their own images.
- **`Platforms/iOS/Resources`.** Presentation, XlsIO, and DocIO ship `LaunchScreen.xib` under that folder. MAUI accepts a `Resources` directory in an app and rejects it in a library (`The name 'Resources' is reserved`). The combined build removes those items. Standalone apps still include them.
- **Quick Look native names.** DataGrid, DocIO, PDF, Presentation, and XlsIO each define `PreviewControllerDS`, `QLPreviewItemFileSystem`, and `QLPreviewItemBundle`. iOS registers the class name globally (`MT4118`, reported again as `IL7000`, `MT4116`, `MT2431`, and `NETSDK1144`). `PrefixQuickLookNativeNames` copies only the current platform's files into `obj/` and adds a unique `[Foundation.Register]`. Do not edit the upstream files: that collides on every Syncfusion merge, and including the Mac Catalyst copy during an iOS build compiles the classes twice. `AppDelegate` is already excluded, so its shared native name is not the same problem.
- **LiquidGlass has no Android target.** Its `ProjectReference` is omitted when the host target is Android. The other samples target Android, iOS, and Mac Catalyst.

### Catalog wiring found while building

`MainPageInit` records only the assembly it is given, then tags every sample with that assembly. The host therefore fills `AvailableControlStreamList` from the sample DLLs beside the running app before calling `MainPageInit`, and then points each sample model at the assembly that actually contains its type. The host DLL does not reference those assemblies directly, because no host source uses their types. iOS and Mac Catalyst leave the DLLs beside the host assembly, so the catalog scans that directory. Android Release packs them into the assembly store and `Assembly.Location` is empty, so the host compiles the project-reference names into `SampleAssemblyNames` and loads those assemblies by name. `SampleBrowser.Maui.SmartPdfViewer` and `SampleBrowser.Maui.PdfViewer` both use the control key `PdfViewer`; the second one is stored under its assembly name so both lists survive.

### Distinct host icon

The home screen name is `Syncfusion MAUI` (`ApplicationTitle`).

The mark is the Syncfusion 3×3, squares `#3843A2`, with the rotated block and the square directly under it in `#EF8C34`. That rotated block sits up and to the right of its grid cell. The icon is positioned so the middle square is at the canvas center, then scaled to 85% of the first large version so the rotated block stays inside the iOS mask. `appiconfg.svg` is only that mark. `appicon.svg` is a white fill.

iOS does not use `MauiIcon`. `Platforms/iOS/Assets.xcassets/appicon.appiconset` supplies the appearances actool accepts: default on white, dark on black, and a grayscale tinted image. There is no asset-catalog slot for the Clear appearance. iOS derives Clear from the icon, so a separate transparent image is not compiled. Android uses the same mark with `ForegroundScale` `0.60` because the adaptive-icon mask would clip the rotated block. Other platforms use the full-size white `MauiIcon`.

The iOS and Mac Catalyst splash is `LaunchScreen.storyboard`: the mark sized to most of the screen, then "Syncfusion" in `#3843A2` and "MAUI Component Demos" in `#EF8C34`. The page background is the system background, so it is white in light mode and black in dark mode. The mark image is opaque on that same background, with a dark variant, because a transparent launch-screen image was not drawn. `EnableBlankMauiSplashScreen` is false so MAUI does not also emit an empty launch storyboard. Android keeps the mark as the splash icon. Android 12 and later draw that icon and one branding image, not free-form labels, so the title and subtitle are a branding image at the bottom. The splash background is `#FFFBFE` in light mode and `#1C1B1F` in dark mode, the same colors as the catalog page, and follows the system night mode. The launcher icon is one adaptive icon on white. Android does not have iOS's Default, Dark, Tinted, and Clear slots, so that icon stays the same in dark mode. Windows keeps a `MauiSplashScreen` of the mark.

### Empty license file, skip-worktree after staging

The host embeds its own `SyncfusionLicense.txt`. Create it empty, `git add` that empty blob, then `git update-index --skip-worktree` on the path. Later edits in the worktree, including a real key, stay out of `git status` and commits. The index blob remains empty.

`skip-worktree` is per clone. The README tells the next person to run it again after pasting a key. Before any control is created, the host calls `BaseConfig.RegisterLicense` with the host assembly and that file. 1Password is not involved.

### Personal wildcard Apple profile, secrets local

iOS and Mac Catalyst signing uses the owner's personal Apple Developer team and a wildcard provisioning profile already on that Mac. The committed host `ApplicationId` is a new bundle id covered by that wildcard. It is not a secret. The app binary will contain it.

`CodesignKey`, `CodesignProvision`, and `DevelopmentTeam` live only in a gitignored `signing.props` next to the host project. The host imports it when the file exists. A `signing.props.example` with empty values is committed so the shape is visible. `.gitignore` ignores `signing.props`, `*.mobileprovision`, `*.p12`, and `*.p8`.

Sample apps do not commit signing material today. This follows that pattern and adds the ignore rules so a personal profile cannot land in the public fork by accident.

### README preamble only

Add a short block at the top of `README.md` that names `SampleBrowser.Maui.Combined.sln` and points at the combined project. State that the committed license file is empty and that Apple certificates and profiles stay on the machine, out of the repository. Leave the upstream catalog below it unchanged.

## Risks / Trade-offs

- [Stock `AvailableControlStreamList` may only filter one assembly] → Spike first. Fall back to a `NavigationPage` plus `CollectionView` that still shows one sample and releases it on pop.
- [Stock navigation may cache visited samples] → Same fallback, where the host owns pop and calls `DisconnectHandler`.
- [A direct project build does not set `SolutionName`] → Meteor's combined launch passes `-p:CombinedSampleBrowser=true`. Do not rely on the solution name alone.
- [Document samples fail `NU1902` / `NU1903` when audit is on] → Disable `NuGetAudit` only for the combined switch. Do not edit those `.csproj` files to clear the warnings.
- [MAUI may still package a referenced sample as an app] → The targets file forces `OutputType` `Library` for the combined solution. Confirm a sample project in that build does not produce an app package.
- [Excluding entry points can drop a newly added upstream startup file] → Exclude by the known startup filenames, not whole `Platforms/` trees. Revisit the exclude list when a combined build fails on a duplicate entry point.
- [First open of a heavy sample stalls the UI thread] → Accepted. Show the busy indicator. Do not keep the previous sample to hide the stall.
- [The combined binary includes every control package] → Accepted. The pilot build is the full solution; fix link or packaging failures as they appear rather than splitting the host.
- [`skip-worktree` is easy to forget on a new clone] → The committed file stays empty, and the README says to mark the file again after pasting a key. `git status` will show the key if that step is skipped.
- [A wildcard profile only covers bundle ids under its prefix] → Choose the host `ApplicationId` to match the owner's existing wildcard. Do not commit the profile itself.
- [iOS Clear icon mode has no image slot] → Default, Dark, and Tinted are supplied. Clear is the system treatment of that icon.

## Migration Plan

The change is additive. Reverting it means deleting the host project, the root solution, the two `Directory.Build` files, and the README preamble. Existing per-control apps do not change. A local license key and `signing.props` exist only in the worktree.

## Open Questions

None. The stock-catalog spike is an implementation step with a specified fallback, not an open product decision.
