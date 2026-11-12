# Spec Delta

## Purpose

One app hosts every control demo in this repository, shows a single sample at a time, and leaves each per-control app buildable on its own.

## ADDED Requirements

### Requirement: Combined catalog
The combined app SHALL present every control demo in this repository in one catalog.

#### Scenario: Launch shows every control
- **WHEN** the user launches the combined app
- **THEN** the catalog includes each control that has its own sample project

#### Scenario: Open a sample
- **WHEN** the user selects a sample from the catalog
- **THEN** that sample is shown

### Requirement: One live sample
The combined app SHALL keep at most the sample the user is viewing. It SHALL release the previous sample, including native resources, when the user leaves it. It SHALL NOT create the next sample before the user selects it.

#### Scenario: Leave a sample
- **WHEN** the user navigates from one sample to another
- **THEN** the sample they left is no longer retained

#### Scenario: Heavy sample still starts cold
- **WHEN** the user selects a sample that has not been created yet
- **THEN** the app indicates that the sample is loading and does not keep an earlier sample resident to shorten that wait

### Requirement: Standalone apps stay buildable
Building an existing per-control solution SHALL still produce that control's standalone app. The combined solution SHALL build the combined app without modifying upstream sample project files.

#### Scenario: Per-control solution
- **WHEN** a per-control solution is built
- **THEN** that solution produces the standalone executable for that control

#### Scenario: Combined solution
- **WHEN** the root combined solution is built
- **THEN** it produces the combined app and the sample project files match their upstream contents

### Requirement: README preamble
The repository README SHALL identify the combined app in a preamble at the top of the file and SHALL leave the upstream controls catalog in place below that preamble.

#### Scenario: Reader opens the README
- **WHEN** a reader opens `README.md`
- **THEN** the top of the file explains how to open the combined app and the existing catalog follows

#### Scenario: Preamble warns about local secrets
- **WHEN** a reader opens `README.md`
- **THEN** the preamble states that the committed license file is empty and that Apple certificates and profiles are not in the repository

### Requirement: Distinct app icon
The combined app icon SHALL show the Syncfusion square mark in `#3843A2`, with the rotated block and the square beneath it in `#EF8C34`. The middle square of the 3×3 grid SHALL be centered in the icon. The name under the icon SHALL be `Syncfusion MAUI`. On iOS, the default appearance SHALL use a white background, the dark appearance a black background, and the tinted appearance a grayscale mark.

#### Scenario: Installed icon
- **WHEN** the combined app is installed
- **THEN** its icon is the purple-and-orange Syncfusion mark, distinct from the standalone demos' purple-background icons, and the home screen name is `Syncfusion MAUI`

### Requirement: Host license file
The combined app SHALL read its Syncfusion license from its own license file. The copy stored in git SHALL be empty. A license key written only in the local working tree SHALL NOT appear in a commit.

#### Scenario: Fresh clone
- **WHEN** someone clones the repository
- **THEN** the host license file is present and empty

#### Scenario: Local license key
- **WHEN** a developer writes a license key into the local license file
- **THEN** committing the repository does not include that key

### Requirement: Local Apple signing
iOS and Mac Catalyst builds of the combined app SHALL be signed with the owner's personal Apple Developer wildcard profile. The repository SHALL NOT contain Apple certificates, provisioning profiles, private keys, or team credentials.

#### Scenario: Public repository
- **WHEN** the fork is published
- **THEN** it contains no Apple certificate, provisioning profile, or private key

#### Scenario: Device build
- **WHEN** the owner builds the combined app for an Apple device
- **THEN** signing uses their personal wildcard profile from the local machine
