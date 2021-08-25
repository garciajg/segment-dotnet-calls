# Dotnet Segment Implementation

Set up a `writeKey` env variable:

You can create a `.env` file and add

```
writeKey=<<myKey>>
```

or simply run this in your terminal

```bash
export writeKey=<<myKey>>
````

On `Program.cs`, there is a function called `RunAnalytics` that will execute all track and identity events. These are the calls it will make:

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
- `IdentifyOnSelfIdentificationSurvery`
  - The `identify` call when the user filled out their military status on self identification survery.

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
- TrackJobPostingApplied`
  - The `track` call when the user applies for a job from the job detail page

Post-Interview Track Events
- `TrackTaxCreditStarted`
  - The `track` call when the user starts Tax Credit
- `TrackTaxCreditQuestionnaireStarted`
  - The `track` call when the user starts the Tax Credit Questionnair
- `TrackSelfIdentificationSurveyStarted`
  - The `track` call when the user start Self Identification Survery
- `TrackSelfIdentificationSurveyCompleted`
  - The `track` call when the user completes Self Identification Survey
- `TrackDocumentationStarted`
  - The `track` call when the user starts documentation
- `TrackFormI9Started`
  - The `track` call when the user starts Form I9
- `TrackSafetyVideoStarted`
  - The `track` call when the user starts start Safety Video section

- `TrackSafetyVideoQuestion1Completed`
  - The `track` call when the user starts Safety Video Questionnaire Part 1

- `TrackSafetyVideoQuestion2Completed`
  - The `track` call when the user starts Safety Video Questionnaire Part 2

- `TrackSafetyVideoCompleted`
  - The `track` call when the user completes Safety Video section

- `TrackEssentialFunctionsStarted`
  - The `track` call when the user starts Essential Functions

- `TrackEssentialFunctionsCompleted`
  - The `track` call when the user completes Essential Functions

- `TrackReferAFriendStarted`
  - The `track` call when the user starts Refer-a-friend

- `TrackReferAFriendCompleted`
  - The `track` call when the user completes Refer-a-friend

- `TrackChatOpened`
  - The `track` call when the user opens Chat.