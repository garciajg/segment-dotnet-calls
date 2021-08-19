using System;
using System.Linq;
using Segment;
using Traits = Segment.Model.Traits;
using Properties = Segment.Model.Properties;
using Environment = System.Environment;

namespace segment_dotnet
{
    class Program
    {
        static void Main(string[] args)
        {
            var writeKey = Environment.GetEnvironmentVariable("writeKey");
            if (string.IsNullOrWhiteSpace(writeKey)) throw new ArgumentException(nameof(writeKey));

            OnExecute(writeKey);
        }

        private static void OnExecute(string writeKey)
        {
            // If you'd like an async batch request, remove the Config arguments and just leave:
            // Analytics.Initialize(writeKey);
            // Also uncomment Analytics.Client.Dispose() and Analytics.Client.Flush()
            Analytics.Initialize(writeKey, new Config().SetAsync(false));
            Logger.Handlers += LoggerOnHandlers;

            RunAnalytics();
            // sending all pendant messages
            // Analytics.Client.Flush();
            PrintSummary();
            // Analytics.Client.Dispose();
        }

        // Logger handler
        public static Logger.LogHandler LoggerOnHandlers = (level, message, args) =>
        {
            if (args != null)
                message = args.Keys.Aggregate(message,
                    (current, key) => current + $" {"" + key}: {"" + args[key]},");

            Console.WriteLine($"[{level}] {message}");
        };

        // This sets up and will run all Track and Identify calls
        private static void RunAnalytics()
        {
            var userId = Guid.NewGuid().ToString(); // Replace this with the actual user OAE ID
            var brandPlaceHolder = "ProLogistix"; // Replace this with the right brand for each call
            var rand = new Random();
            var preferredLanguages = new string[]{ "english", "spanish" }; // made to get a random value for track and identify calls
            var language = preferredLanguages[rand.Next(preferredLanguages.Length)];
            var trueFalseOptions = new bool[]{ true, false }; // made to get a random value for track and identify calls
            var toBeOrNotToBe = trueFalseOptions[rand.Next(trueFalseOptions.Length)];


            // Account Created Event 
            var methodsOptions = new string[]{ "native", "apple" }; // made to get a random value for track and call
            var method = methodsOptions[rand.Next(methodsOptions.Length)];
            var email = "me@mail.com";

            IdentifyOnSignUp(userId, email);
            TrackAccountCreated(userId, brandPlaceHolder, method);

            // Personal Information Completed Event
            var firstName = "Jan";
            var lastName = "Smith";
            var middleInitial = "S";
            var streetAddress = "1234 Fake St";
            var suiteNumber = "1A";
            var city = "Austin";
            var state = "Texas";
            var postalCode = 12345;
            var country = "US";
            var phone = "5551231234";
            
            IdentifyOnPersonalInformation(userId, language, firstName, lastName, middleInitial, streetAddress, suiteNumber, city, state, postalCode, country, phone);
            TrackPersonalInformationCompleted(userId, brandPlaceHolder, language);

            // Preferences Completed Event
            var branchLocation = "Indianapolis West, Indiana";
            var employmentType = "Full Time";
            var minimumPay = 15.50;
            var payFrequencyOptions = new string[]{ "Hourly", "Annually" }; // made to get a random value for track and identify calls
            var payFrequency = payFrequencyOptions[rand.Next(payFrequencyOptions.Length)];
            var startTime = "9:00AM";
            var desiredShift = "1st shift";
            var typesOfJob = "temporary,direct hire";
            var isOptedIn = trueFalseOptions[rand.Next(trueFalseOptions.Length)];

            IdentifyOnPreferences(userId, branchLocation, employmentType, minimumPay, payFrequency, startTime, desiredShift, typesOfJob, isOptedIn);
            TrackPreferencesCompleted(userId, brandPlaceHolder, branchLocation, employmentType, minimumPay, payFrequency, startTime, desiredShift, typesOfJob, isOptedIn);

            // In Branch Completed Event
            TrackInBranchCompleted(userId, brandPlaceHolder, toBeOrNotToBe);

            // Previous Types of Work Completed Event
            var jobOptions = new string[]{ "Warehouse", "Logistics", "Manufacturing", "Industrial", "Production", "Other" };
            var jobs = String.Join(",", jobOptions.ToArray());

            IdentifyOnPreviousWork(userId, jobs);
            TrackPreviousTypesOfWork(userId, brandPlaceHolder, jobs);

            // Quick Questions Completed Event
            var educationLevelOptions = new string[]{ "High School", "Associates", "Bachelor's Degree" }; // Just making these up, not sure what all the options are
            var educationLevel = educationLevelOptions[rand.Next(educationLevelOptions.Length)];
            var is18 = trueFalseOptions[rand.Next(trueFalseOptions.Length)];
            var transportation = trueFalseOptions[rand.Next(trueFalseOptions.Length)];
            var drugScreen = trueFalseOptions[rand.Next(trueFalseOptions.Length)];

            IdentifyOnQuickQuestions(userId, educationLevel, is18, transportation, drugScreen);
            TrackQuickQuestionsCompleted(userId, brandPlaceHolder, educationLevel, is18, transportation, drugScreen);

            // Experience Completed Event
            var jobTitleOptions = new string[]{ "Samsung Warehouse", "Amazon Warehouse", "Truck Driver" };
            var jobTitle = jobTitleOptions[rand.Next(jobTitleOptions.Length)];
            var pastCompName = "Swift Co.";
            var startDate = new DateTime(2020, 02, 28).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            var endDate = new DateTime(2021, 05, 10).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            var currentlyThere = trueFalseOptions[rand.Next(trueFalseOptions.Length)];
            var skillsOptions = new string[]{ 
                "Administrative/Clerical", "Level 1", "Management & Supervision", "Quality Inspection", 
                "Warehouse", "Work Environment", "Warehouse Environment", "Shipping/Receiving Dept",
                "Non-warehouse", "Maintenance & Support functions", "Forklift or power industrial equipment"    
            };
            var skills = String.Join(",", skillsOptions.ToArray());

            IdentifyOnExperience(userId, jobTitle, pastCompName, startDate, endDate, currentlyThere, skills);
            TrackExperienceCompleted(userId, brandPlaceHolder, jobTitle, pastCompName, startDate, endDate, currentlyThere, skills);

            // Leveling Completed Event
            var qOne = "Have you had at least 3 months of warehouse experience within the last 12 months?";
            var qTwo = "Are you looking for ONLY clerical or customer service positions?";
            var qOneAnswer = trueFalseOptions[rand.Next(trueFalseOptions.Length)];
            var qTwoAnswer = trueFalseOptions[rand.Next(trueFalseOptions.Length)];
            TrackLevelingCompleted(userId, brandPlaceHolder, qOne, qOneAnswer, qTwo, qTwoAnswer);

            // Interview Completed Event
            var interviewDate = new DateTime().AddDays(15).ToString("yyyy-MM-ddTHH");
            var interviewTime = new DateTime().AddDays(15).ToString("HH:mm");
            var noTimeSlot = trueFalseOptions[rand.Next(trueFalseOptions.Length)];
            var callForInterview = trueFalseOptions[rand.Next(trueFalseOptions.Length)];

            TrackInterviewScheduled(userId, brandPlaceHolder, interviewDate, interviewTime, noTimeSlot, callForInterview);
        }

        private static void IdentifyOnSignUp(string userId, string email)
        {
            Console.WriteLine("'Idenfity on Signup clicked'\n\tEmail: {0}", email);
            Analytics.Client.Identify(userId, new Traits() {
                { "email", email } // string user's email
            });
        }

        private static void IdentifyOnPersonalInformation(
            string userId, string language, string firstName, string lastName, string middleInitial, string streetAddress,
            string suiteNumber, string city, string state, int postalCode, string country, string phone
        )
        {
            Console.WriteLine("'Idenfity on Signup clicked'");
            Console.WriteLine("\tLanguage: {0}", language);
            Console.WriteLine("\tFirst Name: {0}", firstName);
            Console.WriteLine("\fMiddle Intial: {0}", middleInitial);
            Console.WriteLine("\tLast Name: {0}", lastName);
            Console.WriteLine("\tStreet Address: {0}", streetAddress);
            Console.WriteLine("\fsuite Number: {0}", suiteNumber);
            Console.WriteLine("\tCity: {0}", city);
            Console.WriteLine("\tState: {0}", state);
            Console.WriteLine("\fPostal Code: {0}", postalCode);
            Console.WriteLine("\tCountry: {0}", country);
            Console.WriteLine("\fPhone: {0}", phone);
            Analytics.Client.Identify(userId, new Traits() {
                { "language", language }, // string user's preferred language
                { "first_name", firstName }, // string user's first name
                { "last_name", lastName }, // string user's last name
                { "middle_initial", middleInitial }, // string user's middle initial
                { "phone", phone }, // string user's phone number (discussion needed to talk about format of phone number)
                { "address", new Traits { // address traits
                    { "city", city }, // string user's city location
                    { "street_address", streetAddress }, // string user's streen address
                    { "suite_number", suiteNumber }, // string user's suite number (optional)
                    { "state", state }, // string user's state location
                    { "postal_code", postalCode }, // int user's postal code
                    { "country", country } // string user's country of residence
                }}
            });
        }

        private static void IdentifyOnPreferences(
            string userId, string desiredBranchLocation, string desiredEmploymentType, double desiredMinPay, 
            string desiredPayFrequency, string preferredStartTime, string shifts, string jobTypes, bool isOptedIn
        )
        {
            Console.WriteLine("Identify on Preferences Completed");
            Console.WriteLine("\tUser id: {0}", userId);
            Console.WriteLine("\tDesired Branch Location: {0}", desiredBranchLocation);
            Console.WriteLine("\tDesired Employment Type: {0}", desiredEmploymentType);
            Console.WriteLine("\tDesired Minimum Pay: {0}", desiredMinPay);
            Console.WriteLine("\tDesired Pay Frequency: {0}", desiredPayFrequency);
            Console.WriteLine("\tPreferred Start Time: {0}", preferredStartTime);
            Console.WriteLine("\tShifts: {0}", shifts);
            Console.WriteLine("\tJob Types: {0}", jobTypes);
            Analytics.Client.Identify(userId, new Traits() {
                { "desired_branch_location", desiredBranchLocation }, // text of desired branch location
                { "desired_employment_type", desiredEmploymentType }, // text from employment type field
                { "desired_min_pay", desiredMinPay }, // double from desired minimum pay
                { "desired_pay_frequency", desiredPayFrequency }, // desired pay frequency hourly | annually
                { "preferred_start_time", preferredStartTime }, // text in format HH:MMAM|PM from preferred start time field
                { "shifts", shifts }, // shift for selected preferred start time
                { "job_types", jobTypes }, // comma-separated values of all selections from types of jobs checkmarks
                { "marketing_opt_in", isOptedIn} // boolean did the user aggree to get marketing emails
            });
        }

        private static void IdentifyOnPreviousWork(string userId, string previousTypesOfWork)
        {
            Console.WriteLine("Identify on Previous Types of Work Completed\n\ttUser id: {0}\n\tPrevious Types of Work: {1}", userId, previousTypesOfWork);
            Analytics.Client.Identify(userId, new Traits() {
                { "previous_types_work", previousTypesOfWork } // comma-separated values on the previous types of work selected
            });
        }

        private static void IdentifyOnQuickQuestions(string userId, string highestEducationLevel, bool is18, bool reliableTransportation, bool drugScreen)
        {
            Console.WriteLine("Identify on Quick Questions Completed");
            Console.WriteLine("\tUser id: {0}", userId);
            Console.WriteLine("\tHighest Level of Education: {0}", highestEducationLevel);
            Console.WriteLine("\tis 18: {0}", is18);
            Console.WriteLine("\tReliable Transportation: {0}", reliableTransportation);
            Console.WriteLine("\tDrug Screen: {0}", drugScreen);

            Analytics.Client.Identify(userId, new Traits() {
                { "is_18", is18 }, // boolean is the user 18
                { "reliable_transportation", reliableTransportation }, // boolean does the user have reliable transportation
                { "drug_screen", drugScreen }, // boolean is the user willing to take a drug screen
                { "highest_education_level", highestEducationLevel } // string user's highest level of education
            });
        }

        private static void IdentifyOnExperience(
            string userId, string jobTitle, string pastCompanyName, string pastJobStartDate,
            string pastJobEndDate,bool pastJobCurrentlyThere, string skills
        )
        {
            Console.WriteLine("'Experience Completed'");
            Console.WriteLine("\tUser id: {0}", userId);
            Console.WriteLine("\tJob Title: {0}", jobTitle);
            Console.WriteLine("\tPast Company Name: {0}", pastCompanyName);
            Console.WriteLine("\tPast Job Start Date: {0}", pastJobStartDate);
            Console.WriteLine("\tPast Job End Date: {0}", pastJobEndDate);
            Console.WriteLine("\tIs Currently at Past Job: {0}", pastJobCurrentlyThere);
            Console.WriteLine("\tSkills: {0}", skills);

            Analytics.Client.Identify(userId, new Traits() {
                { "job_title", jobTitle }, // string User's past Job Title
                { "past_company_name", pastCompanyName }, // string User's past company name
                { "past_job_start_date", pastJobStartDate }, // string user's past job start date
                { "past_job_end_date", pastJobEndDate }, // string user's past job end date
                { "past_job_currently_there", pastJobCurrentlyThere }, // boolean Is the user currently working there
                { "skills", skills } // string comma-separated values from checked skills
            });
        }

        private static void TrackAccountCreated(string userId, string brandPlaceHolder, string method)
        {
            Console.WriteLine("'Account Created'\n\tUser id: {0} \n\tBrand: {1} \n\tMethod: {2}", userId, brandPlaceHolder, method);
            Analytics.Client.Track(userId, "Account Created", new Properties() {
                { "source_brand", brandPlaceHolder }, // string brand where event is being tracked from
                { "ga_category", "Account Created" }, // string this will get mapped as a Google Analytics Event Category
                { "method", method } // string one of: 'native' | 'apple',
            });
        }

        private static void TrackPersonalInformationCompleted(string userId, string brandPlaceHolder, string preferredLanguage)
        {
            Console.WriteLine("'Personal Information Completed'\n\ttUser id: {0} \n\tBrand: {1} \n\tLanguage: {2}", userId, brandPlaceHolder, preferredLanguage);
            Analytics.Client.Track(userId, "Personal Information Completed", new Properties() {
                { "source_brand", brandPlaceHolder }, // string brand where event is being tracked from
                { "ga_category", "Account Created" }, // string this will get mapped as a Google Analytics Event Category
                { "preferred_language", preferredLanguage } // string one of: 'english' | 'spanish',
            });
        }

        private static void TrackPreferencesCompleted(
            string userId, string brandPlaceHolder, string desiredBranchLocation,
            string desiredEmploymentType, double desiredMinPay, string desiredPayFrequency,
            string preferredStartTime, string shifts, string jobTypes, bool isOptedIn
        )
        {
            Console.WriteLine("'Preferences Completed");
            Console.WriteLine("\tUser id: {0}", userId);
            Console.WriteLine("\tBrand: {0}", brandPlaceHolder);
            Console.WriteLine("\tDesired Branch Location: {0}", desiredBranchLocation);
            Console.WriteLine("\tDesired Employment Type: {0}", desiredEmploymentType);
            Console.WriteLine("\tDesired Minimum Pay: {0}", desiredMinPay);
            Console.WriteLine("\tDesired Pay Frequency: {0}", desiredPayFrequency);
            Console.WriteLine("\tPreferred Start Time: {0}", preferredStartTime);
            Console.WriteLine("\tShifts: {0}", shifts);
            Console.WriteLine("\tJob Types: {0}", jobTypes);
            Analytics.Client.Track(userId, "Preferences Completed", new Properties() {
                { "source_brand", brandPlaceHolder }, //string  brand where event is being tracked from
                { "ga_category", "Account Created" }, // string this will get mapped as a Google Analytics Event Category
                { "desired_branch_location", desiredBranchLocation }, // string text of desired branch location
                { "desired_employment_type", desiredEmploymentType }, // string text from employment type field
                { "desired_min_pay", desiredMinPay }, // double from desired minimum pay
                { "desired_pay_frequency", desiredPayFrequency }, // string desired pay frequency hourly | annually
                { "preferred_start_time", preferredStartTime }, // string in format HH:MMAM|PM from preferred start time field
                { "shifts", shifts }, // string shift for selected preferred start time
                { "job_types", jobTypes }, // string comma-separated values of all selections from types of jobs checkmarks
                { "marketing_opt_in", isOptedIn } // boolean did the user opted in on marketing emails
            });
        }

        private static void TrackInBranchCompleted(string userId, string brandPlaceHolder, bool inBranch)
        {
            Console.WriteLine("'In Branch Completed'\n\ttUser id: {0} \n\tBrand: {1} \n\tIn Branch: {2}", userId, brandPlaceHolder, inBranch);
            Analytics.Client.Track(userId, "In Branch Completed", new Properties() {
                { "source_brand", brandPlaceHolder }, // string brand where event is being tracked from
                { "ga_category", "Account Created" }, // string this will get mapped as a Google Analytics Event Category
                { "in_branch", inBranch } // boolean was the user in a branch office when the application was filled out
            });
        }

        private static void TrackPreviousTypesOfWork(string userId, string brandPlaceHolder, string previousTypesOfWork)
        {
            Console.WriteLine("'Previous Types of Work Completed'\n\ttUser id: {0} \n\tBrand: {1} \n\tPrevious Types of Work: {2}", userId, brandPlaceHolder, previousTypesOfWork);
            Analytics.Client.Track(userId, "Previous Types of Work Completed", new Properties() {
                { "source_brand", brandPlaceHolder }, // string brand where event is being tracked from
                { "ga_category", "Account Created" }, // string this will get mapped as a Google Analytics Event Category
                { "previous_types_work", previousTypesOfWork } // string comma-separated values on the previous types of work selected
            });
        }

        private static void TrackQuickQuestionsCompleted(string userId, string brandPlaceHolder, string highestEducationLevel, bool is18, bool reliableTransportation, bool drugScreen)
        {
            Console.WriteLine("'Quick Questions Completed'");
            Console.WriteLine("\tUser id: {0}", userId);
            Console.WriteLine("\tBrand: {0}", brandPlaceHolder);
            Console.WriteLine("\tHighest Level of Education: {0}", highestEducationLevel);
            Console.WriteLine("\tis 18: {0}", is18);
            Console.WriteLine("\tReliable Transportation: {0}", reliableTransportation);
            Console.WriteLine("\tDrug Screen: {0}", drugScreen);

            Analytics.Client.Track(userId, "Quick Questions Completed", new Properties() {
                { "source_brand", brandPlaceHolder }, // string brand where event is being tracked from
                { "ga_category", "Account Created" }, // string this will get mapped as a Google Analytics Event Category
                { "is_18", is18 }, // boolean is the user 18
                { "reliable_transportation", reliableTransportation }, // boolean does the user have reliable transportation
                { "drug_screen", drugScreen }, // boolean is the user willing to take a drug screen
                { "highest_education_level", highestEducationLevel } // string user's highest level of education
            });
        }

        private static void TrackExperienceCompleted(
            string userId, string brandPlaceHolder, string jobTitle, 
            string pastCompanyName, string pastJobStartDate, string pastJobEndDate,
            bool pastJobCurrentlyThere, string skills
        )
        {
            Console.WriteLine("'Experience Completed'");
            Console.WriteLine("\tUser id: {0}", userId);
            Console.WriteLine("\tBrand: {0}", brandPlaceHolder);
            Console.WriteLine("\tJob Title: {0}", jobTitle);
            Console.WriteLine("\tPast Company Name: {0}", pastCompanyName);
            Console.WriteLine("\tPast Job Start Date: {0}", pastJobStartDate);
            Console.WriteLine("\tPast Job End Date: {0}", pastJobEndDate);
            Console.WriteLine("\tIs Currently at Past Job: {0}", pastJobCurrentlyThere);
            Console.WriteLine("\tSkills: {0}", skills);

            Analytics.Client.Track(userId, "Experience Completed", new Properties() {
                { "source_brand", brandPlaceHolder }, // string brand where event is being tracked from
                { "ga_category", "Account Created" }, // string this will get mapped as a Google Analytics Event Category
                { "job_title", jobTitle }, // string User's past Job Title
                { "past_company_name", pastCompanyName }, // string User's past company name
                { "past_job_start_date", pastJobStartDate }, // string user's past job start date
                { "past_job_end_date", pastJobEndDate }, // string user's past job end date
                { "past_job_currently_there", pastJobCurrentlyThere }, // boolean Is the user currently working there
                { "skills", skills } // string comma-separated values from checked skills
            });
        }

        private static void TrackLevelingCompleted(
            string userId, string brandPlaceHolder, string questionOne, 
            bool questionOneAnswer, string questionTwo, bool questionTwoAnswer
        )
        {
            Console.WriteLine("'Leveling Completed'");
            Console.WriteLine("\tUser id: {0}", userId);
            Console.WriteLine("\tBrand: {0}", brandPlaceHolder);
            Console.WriteLine("\tQuestion One: {0}", questionOne);
            Console.WriteLine("\tQuestion One Answer: {0}", questionOneAnswer);
            Console.WriteLine("\tQuestion Two: {0}", questionTwo);
            Console.WriteLine("\tQuestion Two Answer: {0}", questionOneAnswer);

            Analytics.Client.Track(userId, "Leveling Completed", new Properties() {
                { "source_brand", brandPlaceHolder }, // brand where event is being tracked from
                { "ga_category", "Account Created" }, // this will get mapped as a Google Analytics Event Category
                { "question_one", questionOne }, // string First question being asked
                { "question_one_answer", questionOneAnswer }, // string Answer to the first question
                { "question_two", questionTwo }, // string Second question being asked
                { "question_two_answer", questionTwoAnswer }, // string answer to the second question
            });
        }

        private static void TrackInterviewScheduled(
            string userId, string brandPlaceHolder, string interviewDate, 
            string interviewTime, bool noTimeSlot, bool callToScheduleInterview
        )
        {
            Console.WriteLine("'Interview Scheduled'");
            Console.WriteLine("\tUser id: {0}", userId);
            Console.WriteLine("\tBrand: {0}", brandPlaceHolder);
            Console.WriteLine("\tInterview Date: {0}", interviewDate);
            Console.WriteLine("\tInterview Time: {0}", interviewTime);
            Console.WriteLine("\tNo Time Slot: {0}", noTimeSlot);
            Console.WriteLine("\tCall to Schedule Interview: {0}", callToScheduleInterview);

            Analytics.Client.Track(userId, "Interview Scheduled", new Properties() {
                { "source_brand", brandPlaceHolder }, // string brand where event is being tracked from
                { "ga_category", "Account Created" }, // string this will get mapped as a Google Analytics Event Category
                { "interview_date", interviewDate }, // string date of the interview
                { "interview_time", interviewTime }, // string time of the interview
                { "no_time_slot", noTimeSlot }, // boolean did the user not find a slot available
                { "call_to_schedule", callToScheduleInterview }, // boolean did the user call to schedule inteview
            });
        }

        private static void DoJourney()
        {
            var anonUserId = Guid.NewGuid().ToString();
            var user1Id = Guid.NewGuid().ToString();
            var user2Id = Guid.NewGuid().ToString();
            var accountId = Guid.NewGuid().ToString();

            AnonymousUserVisitsWebsite(anonUserId);
            UserU1SignsUpForNewTrialAccount(anonUserId, user1Id, accountId);
            UserU1SendInviteToAnotherUserU2(user1Id);
            UserU1SignsOutOfApp(user1Id);
            UserU1SignsBackIntoApp(user1Id);
            TrialsEndsAndUserU1RequestsAccountToBeDeleted(user1Id, user2Id, accountId);
        }

        private static void AnonymousUserVisitsWebsite(string anonUserId)
        {
            //identify page load
            Analytics.Client.Page(anonUserId, "Home");

            //identify anon user
            Analytics.Client.Identify(anonUserId, new Properties
            {
                {"subscription", "inactive"},
            });

            // track CTA click
            Analytics.Client.Track(anonUserId, "CTA Clicked", new Properties
            {
                {"plan", "premium"},
            });
        }

        private static void UserU1SignsUpForNewTrialAccount(string anonUserId, string user1Id, string accountId)
        {
            //page
            Analytics.Client.Page(anonUserId, "Sign Up", new Properties
            {
                {"url", "https://wwww.example.com/sign-up"},
            });

            //new account created
            Analytics.Client.Track(anonUserId, "Account Created", new Properties
            {
                {"account_name", "Acme Inc"},
            });

            //create new user
            Analytics.Client.Track(user1Id, "Signed Up", new Properties
            {
                {"type", "organic"},
                {"first_name", "Peter"},
                {"last_name", "Gibbons"},
                {"email", "pgibbons@initech.com"},
                {"phone", "410-555-9412"},
                {"username", "pgibbons"},
                {"title", "Mr"},
            });

            // alias anon id to new user
            Analytics.Client.Alias(anonUserId, user1Id);

            //add user to account (group)
            Analytics.Client.Group(user1Id, accountId, new Properties
            {
                {"role", "Owner"},
            });

            //confirm track call
            Analytics.Client.Track(user1Id, "Account Added User", new Properties
            {
                {"role", "Owner"},
            });

            //start account trial
            Analytics.Client.Track(user1Id, "Trial Started", new Properties
            {
                {"trial_start_date", DateTime.Now},
                {"trial_end_date", DateTime.Now.AddDays(7)},
                {"trial_plan_name", "premium"},
            });
        }

        private static void UserU1SendInviteToAnotherUserU2(string user1Id)
        {
            //page
            Analytics.Client.Page(user1Id, "Dashboard", new Properties
            {
                {"url", "https://wwww.example.com/dashboard"},
            });

            //invite sent
            Analytics.Client.Track(user1Id, "Invite Sent", new Properties
            {
                {"invitee_email", "janedoe@gmail.com"},
                {"invitee_first_name", "Jane"},
                {"invitee_last_name", "Doe"},
                {"invitee_role", "Admin"},
            });
        }

        private static void UserU1SignsOutOfApp(string user1Id)
        {
            //signed out
            Analytics.Client.Track(user1Id, "Signed Out", new Properties
            {
                {"username", "pgibbons"},
            });
        }

        private static void UserU1SignsBackIntoApp(string user1Id)
        {
            //page
            Analytics.Client.Page(user1Id, "Dashboard", new Properties
            {
                {"url", "https://www.example.com/dashboard"},
            });

            // signed in
            Analytics.Client.Track(user1Id, "Signed In", new Properties
            {
                {"username", "pgibbons"},
            });
        }

        private static void TrialsEndsAndUserU1RequestsAccountToBeDeleted(string user1Id, string user2Id, string accountId)
        {
            //page
            Analytics.Client.Page(user1Id, "Dashboard", new Properties
            {
                {"url", "https://wwww.example.com/account/settings"},
            });

            //trial ended
            Analytics.Client.Track(accountId, "Trial Ended", new Properties
            {
                {"trial_start_date", DateTime.Now},
                {"trial_end_date", DateTime.Now.AddDays(7)},
                {"trial_plan_name", "premium"},
            });

            //track user requests account deletion on upgrade request
            Analytics.Client.Track(user1Id, "Account Delete Requested", new Properties
            {
                {"account_id", accountId},
            });

            //remover User (U2) from account
            Analytics.Client.Track(user2Id, "Account Removed User");

            //remover User (U1) from account
            Analytics.Client.Track(user1Id, "Account Removed User");

            //delete Account
            Analytics.Client.Track(accountId, "Account Deleted", new Properties
            {
                {"account_name", "Acme Inc"},
            });

            //sign out User (U1)
            Analytics.Client.Track(user1Id, "Signed Out", new Properties
            {
                {"username", "pgibbons"},
            });
        }

        public static void PrintSummary()
        {
            Console.WriteLine($"Submitted: {Analytics.Client.Statistics.Submitted}");
            Console.WriteLine($"Failed: {Analytics.Client.Statistics.Failed}");
            Console.WriteLine($"Succeeded: {Analytics.Client.Statistics.Succeeded}");

        }
    }
}
