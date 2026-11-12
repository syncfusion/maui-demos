# Proposal

## Why

The repository ships each Syncfusion .NET MAUI control as its own executable, so comparing controls means launching a different app every time. One host can open every demo in a single process by using the sample browser those projects already boot through `SampleBrowser.Maui.Base`.

## What Changes

- Add a combined MAUI app that fills `BaseConfig.AvailableControlStreamList` from every sample assembly and shows the existing Base main page (`MainPageAndroid`, `MainPageiOS`, `MainPageMacCatalyst`, or `MainPageWindows`). Do not add an AppShell catalog.
- Add a root solution that builds that host together with the sample projects. Leave each per-control solution able to build its own executable.
- Keep upstream sample projects unmodified. A repo-root `Directory.Build.props` and `Directory.Build.targets` apply only when the combined build sets a property: sample projects become libraries and their app entry points drop out, while platform helpers such as save and print services stay.
- Leave `BaseConfig.IsIndividualSB` set to `true` so existing embedded-resource names keep resolving inside each sample assembly.
- Keep at most the active sample alive. When a sample is left, call `SampleView.OnDisappearing` and disconnect its handler before dropping the last reference. Do not prefetch the next sample.
- Give the host its own app icon: the Syncfusion square mark in `#3843A2` with the rotated block and the square beneath it in `#EF8C34`, centered on the middle square, so it is distinct from the standalone demos. The home screen name is `Syncfusion MAUI`.
- Add a host `SyncfusionLicense.txt` that git stores empty. After the blank file is staged, mark it `skip-worktree` so a locally filled key is not committed. 1Password stays out of scope.
- Sign iOS and Mac Catalyst builds with the owner's personal Apple Developer wildcard profile. Keep certificates, provisioning profiles, private keys, and team credentials out of the repository so the fork can be made public.
- Add a short fork preamble at the top of `README.md`. Leave the upstream catalog below it unchanged. The preamble notes the empty license file and that Apple signing stays on the local machine.

## Capabilities

### New Capabilities

- `combined-sample-browser`: One app lists every control demo, opens one sample at a time through the existing Base browser, releases inactive samples, and leaves the individual sample apps buildable.

### Modified Capabilities

- None.

## Impact

- New host project under `MAUI/Combined/`, a new root solution, and new root `Directory.Build.props` / `Directory.Build.targets`.
- `README.md` gains a preamble only. Upstream sample `.csproj` files, sample source, and per-control solutions stay as they are.
- The host adds an app icon, an empty committed license file, and a gitignored local signing file. No Apple certificate, provisioning profile, or private key is added to the repo.
- The host references the existing sample projects and `SampleBrowser.Maui.Base` 35.1.37. Combined builds pass one MSBuild property that individual solutions do not set.
- Startup hitch while a heavy sample is constructed on the UI thread is accepted. The Base page's existing busy indicator covers that wait. Memory is the constraint that wins.
