# Tasks

## 1. Host and solution

- [x] 1.1 Create `MAUI/Combined/SampleBrowser.Maui.Combined/` as a .NET 10 MAUI app with the same target frameworks as the sample apps, referencing `SampleBrowser.Maui.Base` 35.1.37. Set `ApplicationId` to a new bundle id covered by the owner's personal Apple wildcard. Verify the project restores with `dotnet restore` on that project.
- [x] 1.2 Add `SampleBrowser.Maui.Combined.sln` at the repository root, containing the host and every sample `.csproj`. Verify `dotnet sln SampleBrowser.Maui.Combined.sln list` includes the host and all 67 sample projects.
- [x] 1.3 Add the host icon: Syncfusion mark centered on the middle square, squares `#3843A2`, rotated block and the square beneath it `#EF8C34`, with iOS default/dark/tinted appearances. Home screen name is `Syncfusion MAUI`.

## 2. Combined-build switch

- [x] 2.1 Add root `Directory.Build.props` and `Directory.Build.targets` that set `CombinedSampleBrowser` when `$(SolutionName)` is `SampleBrowser.Maui.Combined`, and in that mode turn sample projects into libraries while removing platform startup files and `MauiProgram.cs`. Sample `App.xaml` stays because samples reference that type. Verify `dotnet msbuild` `-getProperty:OutputType` reports `Exe` for `MAUI/Accordion/SampleBrowser.Maui.Accordion/SampleBrowser.Maui.Accordion.csproj` and `Library` when the same project is evaluated with `-p:SolutionName=SampleBrowser.Maui.Combined`.
- [x] 2.2 Build `MAUI/Accordion/SampleBrowser.Maui.Accordion.sln` for one target framework and verify it still produces the Accordion app. Verify a sample project built through `SampleBrowser.Maui.Combined.sln` does not emit an application package.

## 3. Catalog

- [x] 3.1 In the host, set `BaseConfig.IsIndividualSB` to `true`, load every sample assembly's `ControlList.xml` and `*SamplesList.xml` into `BaseConfig.AvailableControlStreamList`, and show the page from `BaseConfig.MainPageInit`. Verify by launching the host on one target that the catalog lists controls from more than one sample assembly, including Accordion and DataGrid.
- [x] 3.2 If that catalog does not list every sample assembly, or the stock page retains a sample after the user leaves it, replace the window page with a `NavigationPage` and a grouped `CollectionView` fed by the same XML, still not a Shell. Verify the same two controls appear and opening one sample shows that sample.

## 4. One live sample

- [x] 4.1 When a sample is left, call `OnDisappearing`, disconnect its handler, and drop the page before creating the next sample. Skip extra work if task 3.1 already shows the stock page does this. Verify by opening two samples in one session that the navigation stack holds only the sample currently on screen.

## 5. License

- [x] 5.1 Add an empty `SyncfusionLicense.txt` embedded in the host, `git add` that empty file, then `git update-index --skip-worktree` on it. Call `BaseConfig.RegisterLicense` with the host assembly before any control is created. Verify `git show` of the index blob is empty, and that writing a non-empty sentinel into the worktree file leaves `git status` clean for that path and does not require a secret manager.

## 6. Apple signing

- [x] 6.1 Commit `signing.props.example` with empty `CodesignKey`, `CodesignProvision`, and `DevelopmentTeam` values, import a gitignored `signing.props` from the host when it exists, and ignore `signing.props`, `*.mobileprovision`, `*.p12`, and `*.p8`. Verify `git check-ignore` matches those paths and `git grep` finds no certificate or provisioning-profile contents under the host.

## 7. README

- [x] 7.1 Add a short preamble at the top of `README.md` that names `SampleBrowser.Maui.Combined.sln` and the combined project, states that the committed license file is empty, and states that Apple certificates and profiles stay on the machine. Leave the upstream catalog below it. Verify the preamble is the first content in the file and the existing "Controls Catalog" section is still present.
