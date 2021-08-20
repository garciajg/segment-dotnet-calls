Set up a `writeKey` env variable:

```bash
export writeKey=<<myKey>>
````

On `Program.cs`, there's a function called `RunAnalytics` that will execute all track and identity events. These are the calls it will make:

**Identify Calls:**
- `IdentifyOnSignUp`
  - The `identify` call when the user clicks on Sign Up button.
- `IdentifyOnPersonalInformation`
  - The `identify` call when the user successfully completed their personal information
- `IdentifyOnPreferences`
  - The `identify` call when the user sucessfully completed filling out their preferrences
- `IdentifyOnPreviousWork`
  - The `identify` call when the user has filled out their previous works.
- `IdentifyOnQuickQuestions`
  - The `identify` call when the user filled out their quick questions.
- `IdentifyOnExperience`
  - The `identify` call when the user filled out their previous work experiences and details.

**Track Calls:**
- `TrackAccountCreated`
  - The `track` call when the user clicks on Sign Up button.
- `TrackPersonalInformationCompleted`
  - The `track` call when the user successfully completed their personal information
- `TrackPreferencesCompleted`
  - The `track` call when the user sucessfully completed filling out their preferrences
- `TrackInBranchCompleted`
  - The `track` call when the user has answer if they are in a branch office or not.
- `TrackPreviousTypesOfWork`
  - The `track` call when the user has filled out their previous works.
- `TrackQuickQuestionsCompleted`
  - The `track` call when the user filled out their quick questions.
- `TrackExperienceCompleted`
  - The `track` call when the user filled out their previous work experiences and details.
- `TrackLevelingCompleted`
  - The `track` call when the user has finished leveling questions.
- `TrackInterviewScheduled`
  - The `track` call when the user has successfully scheduled an interview.
- `TrackJobPostingClicked`
  - The `track` call when the user has clicked on a Job Posting.
- `TrackSignedIn`
  - The `track` call when the user logs in
- `TrackJobSearched`
  - The `track` call when the user searches for a job.
- `TrackDashboardJobApplied`
  - The `track` call when the user applies for a job directly from the list of jobs dashboard.