# Combined sample browser

The app that hosts every demo is `MAUI/Combined/SampleBrowser.Maui.Combined`. Leave the upstream projects under `MAUI/<Control>/` unchanged. Changes belong in the host, `Directory.Build.props`, and `Directory.Build.targets`.

## Which build to run

Use a partial build when the sample libraries for that configuration and target framework are already on disk. That is the usual case, because sample sources are not part of the change.

Build the host and skip every sample project:

```bash
dotnet build MAUI/Combined/SampleBrowser.Maui.Combined/SampleBrowser.Maui.Combined.csproj -c Release -f net10.0-android -p:CombinedSampleBrowser=true -p:BuildProjectReferences=false -p:AndroidSdkDirectory="$HOME/Library/Android/sdk"
```

`-p:CombinedSampleBrowser=true` is required on a project build. The solution sets it from its name. A bare `dotnet build` of the host `.csproj` leaves it unset, and the document samples then fail `NU1902` and `NU1903` as errors.

`-p:BuildProjectReferences=false` does not recompile the samples. The host still copies their DLLs into the app. Those DLLs must already exist under each sample's `bin/<Configuration>/<TargetFramework>/`. Android sample libraries are written there without a runtime-identifier subdirectory. Expect a few minutes: packaging every library is the slow part.

One sample changed: build that project with `-p:CombinedSampleBrowser=true`, then build the host with `-p:BuildProjectReferences=false`.

`Directory.Build.props`, `Directory.Build.targets`, or missing sample output: build the solution so every sample compiles in library mode.

```bash
dotnet build SampleBrowser.Maui.Combined.sln -c Release -f net10.0-android -p:AndroidSdkDirectory="$HOME/Library/Android/sdk"
```

Replace `-c Release` and `-f net10.0-android` for Debug, `net10.0-ios`, or `net10.0-maccatalyst`. iOS and Mac Catalyst read the gitignored `signing.props` beside the host.

## Install

Build once. Install the signed package on each device:

```bash
adb -s <serial> install -r MAUI/Combined/SampleBrowser.Maui.Combined/bin/Release/net10.0-android/net.prestonmccormick.syncfusion.maui.combined-Signed.apk
```

`adb devices -l` lists phones and running emulators. `emulator -list-avds` lists virtual devices. Start one with `emulator -avd <name>`.
