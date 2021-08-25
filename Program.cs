using System;
using System.IO;
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
            var root = Directory.GetCurrentDirectory();
            var dotenv = Path.Combine(root, ".env");
            DotEnv.Load(dotenv);

            var writeKey = Environment.GetEnvironmentVariable("writeKey");
            if (string.IsNullOrWhiteSpace(writeKey)) throw new ArgumentException(nameof(writeKey));

            OnExecute(writeKey);
        }

        private static void OnExecute(string writeKey)
        {
            Analytics.Initialize(writeKey);
            Logger.Handlers += LoggerOnHandlers;

            RunAnalytics();
            // sending all pendant messages
            Analytics.Client.Flush();
            PrintSummary();
            Analytics.Client.Dispose();
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
            var typesOfJob = new string[]{ "temporary", "direct" };
            var isOptedIn = trueFalseOptions[rand.Next(trueFalseOptions.Length)];

            IdentifyOnPreferences(userId, branchLocation, employmentType, minimumPay, payFrequency, startTime, desiredShift, typesOfJob, isOptedIn);
            TrackPreferencesCompleted(userId, brandPlaceHolder, branchLocation, employmentType, minimumPay, payFrequency, startTime, desiredShift, typesOfJob, isOptedIn);

            // In Branch Completed Event
            TrackInBranchCompleted(userId, brandPlaceHolder, toBeOrNotToBe);

            // Previous Types of Work Completed Event
            var jobOptions = new string[]{ "Warehouse", "Logistics", "Manufacturing", "Industrial", "Production", "Other" };
            var jobs = String.Join(",", jobOptions.ToArray());

            IdentifyOnPreviousWork(userId, jobOptions);
            TrackPreviousTypesOfWork(userId, brandPlaceHolder, jobOptions);

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

            IdentifyOnExperience(userId, jobTitle, pastCompName, startDate, endDate, currentlyThere, skillsOptions);
            TrackExperienceCompleted(userId, brandPlaceHolder, jobTitle, pastCompName, startDate, endDate, currentlyThere, skillsOptions);

            // Leveling Completed Event
            var qOne = "warehouse_experience_three_months_last_18_months";
            var qTwo = "forklift_experience_three_months_last_18_months";
            var qOneAnswer = trueFalseOptions[rand.Next(trueFalseOptions.Length)];
            var qTwoAnswer = trueFalseOptions[rand.Next(trueFalseOptions.Length)];
            TrackLevelingCompleted(userId, brandPlaceHolder, qOne, qOneAnswer, qTwo, qTwoAnswer);

            // Interview Completed Event
            var interviewDate = new DateTime().AddDays(15).ToString("yyyy-MM-dd");
            var interviewTime = new DateTime().AddDays(15).ToString("HH:mm");
            var noTimeSlot = trueFalseOptions[rand.Next(trueFalseOptions.Length)];
            var callForInterview = trueFalseOptions[rand.Next(trueFalseOptions.Length)];

            TrackInterviewScheduled(userId, brandPlaceHolder, interviewDate, interviewTime, noTimeSlot, callForInterview);

            // Signed In Event 
            // properties come from the Account Created event
            TrackSignedIn(userId, brandPlaceHolder, method);

            // Job Posting Clicked Event
            var jobTitleClicked = "Warehouse manager";
            var jobLocation = "Austin, TX";
            var jobNumber = rand.Next(10000, 99999); // random number between 10,000 - 99,000
            var employeeType = "Temp";
            var basePay = 20.25;
            var travel = trueFalseOptions[rand.Next(trueFalseOptions.Length)];
            var paidRelocation = trueFalseOptions[rand.Next(trueFalseOptions.Length)];
            var manageOthers = trueFalseOptions[rand.Next(trueFalseOptions.Length)];

            TrackJobPostingClicked(userId, brandPlaceHolder, jobTitleClicked, jobLocation, jobNumber, employeeType, basePay, travel, paidRelocation, manageOthers);

            // Job Posting Applied Event
            TrackJobPostingApplied(userId, brandPlaceHolder, jobTitle, jobLocation, jobNumber, employeeType, basePay, travel, paidRelocation);

            // Job Searched Event
            // Gets triggered when a user searches for a job
            TrackJobSearched(userId, brandPlaceHolder, city, state, jobTitle);

            // Dashboard Job Applied
            // Gets triggered when applied directly from the list of jobs dashboard
            var location = "Dallas";
            TrackDashboardJobApplied(userId, brandPlaceHolder, jobNumber, basePay, location);

            // Tax Credit Started Event
            TrackTaxCreditStarted(userId, brandPlaceHolder);

            // Credit Questionnaire Started
            TrackTaxCreditQuestionnaireStarted(userId, brandPlaceHolder);

            // Self Identification Survey Started
            TrackSelfIdentificationSurveyStarted(userId, brandPlaceHolder);

            // Self Identification Completed
            var genderOptions = new string[]{ "male", "female", "NA" };
            var gender = genderOptions[rand.Next(genderOptions.Length)];
            var raceOptions = new string[]{ "hispanic/latino", "white", "black", "native pacific islander", "two or more", "NA" };
            var race = raceOptions[rand.Next(raceOptions.Length)];
            var militaryVeteranOptions = new string[]{ "yes", "no", "NA" };
            var militaryVeteran = militaryVeteranOptions[rand.Next(militaryVeteranOptions.Length)];

            IdentifyOnSelfIdentificationSurvery(userId, militaryVeteran);
            TrackSelfIdentificationSurveyCompleted(userId, brandPlaceHolder);

            // Documentation Started
            TrackDocumentationStated(userId, brandPlaceHolder);

            // Form I9 Started
            TrackFormI9Started(userId, brandPlaceHolder);

            // Safety Video Started
            TrackSafetyVideoStarted(userId, brandPlaceHolder);

            // Safety Video Questions 1 Completed
            TrackSafetyVideoQuestion1Completed(userId, brandPlaceHolder);

            // Safety Video Questions 2 Completed
            TrackSafetyVideoQuestion2Completed(userId, brandPlaceHolder);

            // Safety Video Completed
            TrackSafetyVideoCompleted(userId, brandPlaceHolder);

            // Essential Functions Started
            TrackEssentialFunctionsStarted(userId, brandPlaceHolder);

            // Essential Functions Completed
            var sit8Hours = trueFalseOptions[rand.Next(trueFalseOptions.Length)];
            var stand8Hours = trueFalseOptions[rand.Next(trueFalseOptions.Length)];
            var repetitiveTasks8Hours = trueFalseOptions[rand.Next(trueFalseOptions.Length)];
            var carryPounds8Hours = rand.Next(10, 100);
            TrackEssentialFunctionsCompleted(userId, brandPlaceHolder, sit8Hours, stand8Hours, repetitiveTasks8Hours, carryPounds8Hours);

            // Refer-a-Friend Started
            TrackReferAFriendStarted(userId, brandPlaceHolder);

            // Refer-a-Friend Completed
            TrackReferAFriendCompleted(userId, brandPlaceHolder);

            // Chat Opened Event
            var isQuestionRecommenedIssue = trueFalseOptions[rand.Next(trueFalseOptions.Length)];
            var anotherQuestion = trueFalseOptions[rand.Next(trueFalseOptions.Length)];
            var userQuestion = "Tax Credit";
            var recommendedIssue = "Did you mean: Questions regarding W-2";
            var preferredLanguage = preferredLanguages[rand.Next(preferredLanguages.Length)];

            TrackChatOpened(userId, brandPlaceHolder, preferredLanguage, userQuestion, recommendedIssue, isQuestionRecommenedIssue, anotherQuestion);
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
            string desiredPayFrequency, string preferredStartTime, string shifts, string[] jobTypes, bool isOptedIn
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
                { "job_types", jobTypes }, // array values of all selections from types of jobs checkmarks
                { "marketing_opt_in", isOptedIn} // boolean did the user aggree to get marketing emails
            });
        }

        private static void IdentifyOnPreviousWork(string userId, string[] previousTypesOfWork)
        {
            Console.WriteLine("Identify on Previous Types of Work Completed\n\ttUser id: {0}\n\tPrevious Types of Work: {1}", userId, previousTypesOfWork);
            Analytics.Client.Identify(userId, new Traits() {
                { "previous_types_work", previousTypesOfWork } // array values on the previous types of work selected
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
            string pastJobEndDate,bool pastJobCurrentlyThere, string[] skills
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
                { "skills", skills } // array values from checked skills
            });
        }

        private static void IdentifyOnSelfIdentificationSurvery(
            string userId, string militaryVeteran
        )
        {
            Console.WriteLine("'Self Identification Survery Completed'");
            Console.WriteLine("\tUser id: {0}", userId);
            Console.WriteLine("\tMilitary Veteran: {0}", militaryVeteran);

            Analytics.Client.Identify(userId, new Traits() {
                { "military_veteran", militaryVeteran } // string user's military veteran
            });
        }

        private static void TrackAccountCreated(string userId, string brandPlaceHolder, string method)
        {
            Console.WriteLine("'Account Created'\n\tUser id: {0} \n\tBrand: {1} \n\tMethod: {2}", userId, brandPlaceHolder, method);

            Analytics.Client.Track(userId, "Account Created", new Properties() {
                { "source_brand", brandPlaceHolder }, // string brand where event is being tracked from
                { "category", "Application" }, // string this will get mapped as a Google Analytics Event Category
                { "label", "Account Created" }, // string this will get mapped as a Google Analytics Event Label
                { "method", method } // string one of: 'native' | 'apple',
            });
        }

        private static void TrackPersonalInformationCompleted(string userId, string brandPlaceHolder, string preferredLanguage)
        {
            Console.WriteLine("'Personal Information Completed'\n\ttUser id: {0} \n\tBrand: {1} \n\tLanguage: {2}", userId, brandPlaceHolder, preferredLanguage);

            Analytics.Client.Track(userId, "Personal Information Completed", new Properties() {
                { "source_brand", brandPlaceHolder }, // string brand where event is being tracked from
                { "category", "Application" }, // string this will get mapped as a Google Analytics Event Category
                { "label", "Personal Information" }, // string this will get mapped as a Google Analytics Event Label
                { "preferred_language", preferredLanguage } // string one of: 'english' | 'spanish',
            });
        }

        private static void TrackPreferencesCompleted(
            string userId, string brandPlaceHolder, string desiredBranchLocation,
            string desiredEmploymentType, double desiredMinPay, string desiredPayFrequency,
            string preferredStartTime, string shifts, string[] jobTypes, bool isOptedIn
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
                { "category", "Application" }, // string this will get mapped as a Google Analytics Event Category
                { "label", "Preferences" }, // string this will get mapped as a Google Analytics Event Label
                { "desired_branch_location", desiredBranchLocation }, // string text of desired branch location
                { "desired_employment_type", desiredEmploymentType }, // string text from employment type field
                { "desired_min_pay", desiredMinPay }, // double from desired minimum pay
                { "desired_pay_frequency", desiredPayFrequency }, // string desired pay frequency hourly | annually
                { "preferred_start_time", preferredStartTime }, // string in format HH:MMAM|PM from preferred start time field
                { "shifts", shifts }, // string shift for selected preferred start time
                { "job_types", jobTypes }, // array values of all selections from types of jobs checkmarks
                { "marketing_opt_in", isOptedIn } // boolean did the user opted in on marketing emails
            });
        }

        private static void TrackInBranchCompleted(string userId, string brandPlaceHolder, bool inBranch)
        {
            Console.WriteLine("'In Branch Completed'\n\ttUser id: {0} \n\tBrand: {1} \n\tIn Branch: {2}", userId, brandPlaceHolder, inBranch);

            Analytics.Client.Track(userId, "In Branch Completed", new Properties() {
                { "source_brand", brandPlaceHolder }, // string brand where event is being tracked from
                { "category", "Application" }, // string this will get mapped as a Google Analytics Event Category
                { "label", "In Branch" }, // string this will get mapped as a Google Analytics Event Label
                { "in_branch", inBranch } // boolean was the user in a branch office when the application was filled out
            });
        }

        private static void TrackPreviousTypesOfWork(string userId, string brandPlaceHolder, string[] previousTypesOfWork)
        {
            Console.WriteLine("'Previous Types of Work Completed'\n\ttUser id: {0} \n\tBrand: {1} \n\tPrevious Types of Work: {2}", userId, brandPlaceHolder, previousTypesOfWork);

            Analytics.Client.Track(userId, "Previous Types of Work Completed", new Properties() {
                { "source_brand", brandPlaceHolder }, // string brand where event is being tracked from
                { "category", "Application" }, // string this will get mapped as a Google Analytics Event Category
                { "label", "Previous Types of Work" }, // string this will get mapped as a Google Analytics Event Label
                { "previous_types_work", previousTypesOfWork } // array values on the previous types of work selected
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
                { "category", "Application" }, // string this will get mapped as a Google Analytics Event Category
                { "label", "Quick Questions" }, // string this will get mapped as a Google Analytics Event Label
                { "is_18", is18 }, // boolean is the user 18
                { "reliable_transportation", reliableTransportation }, // boolean does the user have reliable transportation
                { "drug_screen", drugScreen }, // boolean is the user willing to take a drug screen
                { "highest_education_level", highestEducationLevel } // string user's highest level of education
            });
        }

        private static void TrackExperienceCompleted(
            string userId, string brandPlaceHolder, string jobTitle, 
            string pastCompanyName, string pastJobStartDate, string pastJobEndDate,
            bool pastJobCurrentlyThere, string[] skills
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
                { "category", "Application" }, // string this will get mapped as a Google Analytics Event Category
                { "label", "Experience" }, // string this will get mapped as a Google Analytics Event Label
                { "job_title", jobTitle }, // string User's past Job Title
                { "past_company_name", pastCompanyName }, // string User's past company name
                { "past_job_start_date", pastJobStartDate }, // string user's past job start date
                { "past_job_end_date", pastJobEndDate }, // string user's past job end date
                { "past_job_currently_there", pastJobCurrentlyThere }, // boolean Is the user currently working there
                { "skills", skills } // array values from checked skills
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
                { "category", "Application" }, // this will get mapped as a Google Analytics Event Category
                { "label", "Leveling" }, // string this will get mapped as a Google Analytics Event Label
                { questionOne, questionOneAnswer }, // boolean First question being asked
                { questionTwo, questionTwoAnswer }, // boolean answer to the second question
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
                { "category", "Application" }, // string this will get mapped as a Google Analytics Event Category
                { "label", "Interview" }, // string this will get mapped as a Google Analytics Event Label
                { "interview_date", interviewDate }, // string date of the interview
                { "interview_time", interviewTime }, // string time of the interview
                { "no_time_slot", noTimeSlot }, // boolean did the user not find a slot available
                { "call_to_schedule", callToScheduleInterview }, // boolean did the user call to schedule inteview
            });
        }

        private static void TrackJobPostingClicked(
            string userId, string brandPlaceHolder, string jobTitle, string jobLocation,
            int jobNumber, string employeeType, double basePay, bool travel, 
            bool paidRelocation, bool manageOthers
        ) 
        {
            Console.WriteLine("'Job Posting Clicked'");
            Console.WriteLine("\tUser id: {0}", userId);
            Console.WriteLine("\tBrand: {0}", brandPlaceHolder);
            Console.WriteLine("\tJob Title: {0}", jobTitle);
            Console.WriteLine("\tJob Location: {0}", jobLocation);
            Console.WriteLine("\tJob Number: {0}", jobNumber);
            Console.WriteLine("\tEmployee Type: {0}", employeeType);
            Console.WriteLine("\tBase Pay: {0}", basePay);
            Console.WriteLine("\tTravel: {0}", travel);
            Console.WriteLine("\tPaid Relocation: {0}", paidRelocation);
            Console.WriteLine("\tManage Others: {0}", manageOthers);
 
            Analytics.Client.Track(userId, "Job Posting Clicked", new Properties() {
                { "source_brand", brandPlaceHolder }, //string  brand where event is being tracked from
                { "category", "Job Posting" }, // string this will get mapped as a Google Analytics Event Category
                { "label", "Job Posting" }, // string this will get mapped as a Google Analytics Event Label
                { "job_title", jobTitle }, // string Job title
                { "job_location", jobLocation }, // string Location of the job
                { "job_number", jobNumber }, // int Identifying job number
                { "employee_type", employeeType }, // string employee type
                { "base_pay", basePay }, // double base pay for the job
                { "travel", travel }, // boolean does the job require travel
                { "paid_relocation", paidRelocation }, // boolean does job pay for relocation
                { "manage_others", manageOthers} // boolean does the job require to manage others
            });
        }

        private static void TrackJobPostingApplied(
            string userId, string brandPlaceHolder, string jobTitle, string jobLocation,
            int jobNumber, string employeeType, double basePay, bool travel, 
            bool paidRelocation
        ) 
        {
            Console.WriteLine("'Job Posting Applied'");
            Console.WriteLine("\tUser id: {0}", userId);
            Console.WriteLine("\tBrand: {0}", brandPlaceHolder);
            Console.WriteLine("\tJob Title: {0}", jobTitle);
            Console.WriteLine("\tJob Location: {0}", jobLocation);
            Console.WriteLine("\tJob Number: {0}", jobNumber);
            Console.WriteLine("\tEmployee Type: {0}", employeeType);
            Console.WriteLine("\tBase Pay: {0}", basePay);
            Console.WriteLine("\tTravel: {0}", travel);
            Console.WriteLine("\tPaid Relocation: {0}", paidRelocation);
 
            Analytics.Client.Track(userId, "Job Posting Applied", new Properties() {
                { "source_brand", brandPlaceHolder }, //string  brand where event is being tracked from
                { "category", "Job Posting" }, // string this will get mapped as a Google Analytics Event Category
                { "label", "Job Applied" }, // string this will get mapped as a Google Analytics Event Label
                { "job_title", jobTitle }, // string Job title
                { "job_location", jobLocation }, // string Location of the job
                { "job_number", jobNumber }, // int Identifying job number
                { "employee_type", employeeType }, // string employee type
                { "base_pay", basePay }, // double base pay for the job
                { "travel", travel }, // boolean does the job require travel
                { "paid_relocation", paidRelocation }, // boolean does job pay for relocation
            });
        }

        private static void TrackSignedIn(string userId, string brandPlaceHolder, string method)
        {
            Console.WriteLine("'Signed In'\n\tUser id: {0} \n\tBrand: {1} \n\tMethod: {2}", userId, brandPlaceHolder, method);

            Analytics.Client.Track(userId, "Signed In", new Properties() {
                { "source_brand", brandPlaceHolder }, // string brand where event is being tracked from
                { "category", "Login" }, // string this will get mapped as a Google Analytics Event Category
                { "label", "Login" }, // string this will get mapped as a Google Analytics Event Label
                { "method", method } // string one of: 'native' | 'apple',
            });
        }

        // Gets triggered when a user searches for a job
        private static void TrackJobSearched(string userId, string brandPlaceHolder, string city, string state, string jobTitle)
        {
            Console.WriteLine("'Job Searched'");
            Console.WriteLine("\tUser id: {0}", userId);
            Console.WriteLine("\tBrand: {0}", brandPlaceHolder);
            Console.WriteLine("\tJob Title: {0}", jobTitle);
            Console.WriteLine("\tCity: {0}", city);
            Console.WriteLine("\tState: {0}", state);
 
            Analytics.Client.Track(userId, "Job Searched", new Properties() {
                { "source_brand", brandPlaceHolder }, //string  brand where event is being tracked from
                { "category", "Job Posting" }, // string this will get mapped as a Google Analytics Event Category
                { "label", "Job Search" }, // string this will get mapped as a Google Analytics Event Label
                { "job_title", jobTitle }, // string Job title searched for
                { "city", city }, // string City searched for jobs
                { "state", state }, // string State searched for jobs
            });
        }

        // Gets triggered when applied directly from the list of jobs dashboard
        private static void TrackDashboardJobApplied(string userId, string brandPlaceHolder, int jobNumber, double basePay, string location)
        {
            Console.WriteLine("'Dashboard Job Applied'");
            Console.WriteLine("\tUser id: {0}", userId);
            Console.WriteLine("\tBrand: {0}", brandPlaceHolder);
            Console.WriteLine("\tJob Number: {0}", jobNumber);
            Console.WriteLine("\tBase Pay: {0}", basePay);
            Console.WriteLine("\tLocation: {0}", location);
 
            Analytics.Client.Track(userId, "Dashboard Job Applied", new Properties() {
                { "source_brand", brandPlaceHolder }, //string  brand where event is being tracked from
                { "category", "Job Posting" }, // string this will get mapped as a Google Analytics Event Category
                { "label", "Job Posting" }, // string this will get mapped as a Google Analytics Event Label
                { "job_number", jobNumber }, // int Job number
                { "base_pay", basePay }, // double Base pay offered
                { "location", location }, // string Location of the job applying for
            });
        }

        private static void TrackTaxCreditStarted(string userId, string brandPlaceHolder)
        {
            Console.WriteLine("'Tax Credit Started'\n\ttUser id: {0} \n\tBrand: {1}", userId, brandPlaceHolder);

            Analytics.Client.Track(userId, "Tax Credit Started", new Properties() {
                { "source_brand", brandPlaceHolder }, // string brand where event is being tracked from
                { "category", "Onboarding" }, // string this will get mapped as a Google Analytics Event Category
                { "label", "Tax Credit" }, // string this will get mapped as a Google Analytics Event Label 
            });
        }

        private static void TrackTaxCreditQuestionnaireStarted(string userId, string brandPlaceHolder)
        {
            Console.WriteLine("'Tax Credit Questionnaire Started'\n\ttUser id: {0} \n\tBrand: {1}", userId, brandPlaceHolder);

            Analytics.Client.Track(userId, "Tax Credit Questionare Started", new Properties() {
                { "source_brand", brandPlaceHolder }, // string brand where event is being tracked from
                { "category", "Onboarding" }, // string this will get mapped as a Google Analytics Event Category
                { "label", "Tax Credit" }, // string this will get mapped as a Google Analytics Event Label
            });
        }

        private static void TrackSelfIdentificationSurveyStarted(string userId, string brandPlaceHolder)
        {
            Console.WriteLine("'Self Identification Survery Started'\n\ttUser id: {0} \n\tBrand: {1}", userId, brandPlaceHolder);

            Analytics.Client.Track(userId, "Self Identification Survery Started", new Properties() {
                { "source_brand", brandPlaceHolder }, // string brand where event is being tracked from
                { "category", "Onboarding" }, // string this will get mapped as a Google Analytics Event Category
                { "label", "Self Identification Survery" }, // string this will get mapped as a Google Analytics Event Label
            });
        }

        private static void TrackSelfIdentificationSurveyCompleted(string userId, string brandPlaceHolder)
        {
            Console.WriteLine("'Self Identification Survery Completed'\n\ttUser id: {0} \n\tBrand: {1}", userId, brandPlaceHolder);

            Analytics.Client.Track(userId, "Self Identification Survery Completed", new Properties() {
                { "source_brand", brandPlaceHolder }, // string brand where event is being tracked from
                { "category", "Onboarding" }, // string this will get mapped as a Google Analytics Event Category
                { "label", "Self Identification Survery" }, // string this will get mapped as a Google Analytics Event Label
            });
        }

        private static void TrackDocumentationStated(string userId, string brandPlaceHolder)
        {
            Console.WriteLine("'Documentation Started'\n\ttUser id: {0} \n\tBrand: {1}", userId, brandPlaceHolder);

            Analytics.Client.Track(userId, "Documentation Started", new Properties() {
                { "source_brand", brandPlaceHolder }, // string brand where event is being tracked from
                { "category", "Onboarding" }, // string this will get mapped as a Google Analytics Event Category
                { "label", "Documentation" }, // string this will get mapped as a Google Analytics Event Label
            });
        }

        private static void TrackFormI9Started(string userId, string brandPlaceHolder)
        {
            Console.WriteLine("'Form I9 Started'\n\ttUser id: {0} \n\tBrand: {1}", userId, brandPlaceHolder);

            Analytics.Client.Track(userId, "Form I9 Started", new Properties() {
                { "source_brand", brandPlaceHolder }, // string brand where event is being tracked from
                { "category", "Onboarding" }, // string this will get mapped as a Google Analytics Event Category
                { "label", "Form I9" }, // string this will get mapped as a Google Analytics Event Label
            });
        }

        private static void TrackSafetyVideoStarted(string userId, string brandPlaceHolder)
        {
            Console.WriteLine("'Safety Video Started'\n\ttUser id: {0} \n\tBrand: {1}", userId, brandPlaceHolder);

            Analytics.Client.Track(userId, "Safety Video Started", new Properties() {
                { "source_brand", brandPlaceHolder }, // string brand where event is being tracked from
                { "category", "Onboarding" }, // string this will get mapped as a Google Analytics Event Category
                { "label", "Safety Video" }, // string this will get mapped as a Google Analytics Event Label
            });
        }

        private static void TrackSafetyVideoQuestion1Completed(string userId, string brandPlaceHolder)
        {
            Console.WriteLine("'Safety Video Question 1 Completed'\n\ttUser id: {0} \n\tBrand: {1}", userId, brandPlaceHolder);

            Analytics.Client.Track(userId, "Safety Video Question 1 Completed", new Properties() {
                { "source_brand", brandPlaceHolder }, // string brand where event is being tracked from
                { "category", "Onboarding" }, // string this will get mapped as a Google Analytics Event Category
                { "label", "Safety Video" }, // string this will get mapped as a Google Analytics Event Label
            });
        }

        private static void TrackSafetyVideoQuestion2Completed(string userId, string brandPlaceHolder)
        {
            Console.WriteLine("'Safety Video Question 2 Completed'\n\ttUser id: {0} \n\tBrand: {1}", userId, brandPlaceHolder);

            Analytics.Client.Track(userId, "Safety Video Question 2 Completed", new Properties() {
                { "source_brand", brandPlaceHolder }, // string brand where event is being tracked from
                { "category", "Onboarding" }, // string this will get mapped as a Google Analytics Event Category
                { "label", "Safety Video" }, // string this will get mapped as a Google Analytics Event Label
            });
        }

        private static void TrackSafetyVideoCompleted(string userId, string brandPlaceHolder)
        {
            Console.WriteLine("'Safety Video Completed'\n\ttUser id: {0} \n\tBrand: {1}", userId, brandPlaceHolder);

            Analytics.Client.Track(userId, "Safety Video Completed", new Properties() {
                { "source_brand", brandPlaceHolder }, // string brand where event is being tracked from
                { "category", "Onboarding" }, // string this will get mapped as a Google Analytics Event Category
                { "label", "Safety Video" }, // string this will get mapped as a Google Analytics Event Label
            });
        }

        private static void TrackEssentialFunctionsStarted(string userId, string brandPlaceHolder)
        {
            Console.WriteLine("'Essenstial Functions Started'\n\ttUser id: {0} \n\tBrand: {1}", userId, brandPlaceHolder);

            Analytics.Client.Track(userId, "Essenstial Functions Started", new Properties() {
                { "source_brand", brandPlaceHolder }, // string brand where event is being tracked from
                { "category", "Onboarding" }, // string this will get mapped as a Google Analytics Event Category
                { "label", "Essential Functions" }, // string this will get mapped as a Google Analytics Event Label
            });
        }

        private static void TrackEssentialFunctionsCompleted(
            string userId, string brandPlaceHolder, bool sit8Hours, bool stand8Hours,
            bool repetitiveTasks8Hours, int carryPounds8Hours
        )
        {
            Console.WriteLine("'Essential Functions Completed'");
            Console.WriteLine("\tUser id: {0}", userId);
            Console.WriteLine("\tBrand: {0}", brandPlaceHolder);
            Console.WriteLine("\tSit 8 Hours: {0}", sit8Hours);
            Console.WriteLine("\tStand 8 hours: {0}", stand8Hours);
            Console.WriteLine("\tRepetitive Tasks 8 Hours: {0}", repetitiveTasks8Hours);
            Console.WriteLine("\tCarry Pounds 8 hours: {0}", carryPounds8Hours);

            Analytics.Client.Track(userId, "Essential Functions Completed", new Properties() {
                { "source_brand", brandPlaceHolder }, // string brand where event is being tracked from
                { "category", "Onboarding" }, // string this will get mapped as a Google Analytics Event Category
                { "label", "Essential Functions" }, // string this will get mapped as a Google Analytics Event Label
                { "sit_8_hours", sit8Hours }, // boolean is the user willing to sit for 8 hours
                { "stand_8_hours", stand8Hours }, // boolean is the user willing to stand for 8 hours
                { "repetitive_tasks_8_hours", repetitiveTasks8Hours }, // boolean is the user willing to do repetitive tasks for 8 hours
                { "carry_pounds_8_hours", carryPounds8Hours }, // int how many pounds can the user carry for 8 hours
            });
        }

        private static void TrackReferAFriendStarted(string userId, string brandPlaceHolder)
        {
            Console.WriteLine("'Refer-a-Friend Started'\n\ttUser id: {0} \n\tBrand: {1}", userId, brandPlaceHolder);

            Analytics.Client.Track(userId, "Refer-a-Friend Started", new Properties() {
                { "source_brand", brandPlaceHolder }, // string brand where event is being tracked from
                { "category", "Onboarding" }, // string this will get mapped as a Google Analytics Event Category
                { "label", "Refer-a-Friend" }, // string this will get mapped as a Google Analytics Event Label
            });
        }

        private static void TrackReferAFriendCompleted(string userId, string brandPlaceHolder)
        {
            Console.WriteLine("'Refer-a-Friend Completed'\n\ttUser id: {0} \n\tBrand: {1}", userId, brandPlaceHolder);

            Analytics.Client.Track(userId, "Refer-a-Friend Completed", new Properties() {
                { "source_brand", brandPlaceHolder }, // string brand where event is being tracked from
                { "category", "Onboarding" }, // string this will get mapped as a Google Analytics Event Category
                { "label", "Refer-a-Friend" }, // string this will get mapped as a Google Analytics Event Label
            });
        }

        private static void TrackChatOpened(string userId, string brandPlaceHolder,
            string preferredLanguage, string userQuestion, string recommenededIssue, bool isQuestionRecommenedIssue, bool anotherQuestion)
        {
            Console.WriteLine("'Chat Opened'");
            Console.WriteLine("\tUser id: {0}", userId);
            Console.WriteLine("\tBrand: {0}", brandPlaceHolder);
            Console.WriteLine("\tPreferred Language: {0}", preferredLanguage);
            Console.WriteLine("\tUser Question: {0}", userQuestion);
            Console.WriteLine("\tRecommended Issue: {0}", recommenededIssue);
            Console.WriteLine("\tIs Question Recommened Issue: {0}", isQuestionRecommenedIssue);
            Console.WriteLine("\tAnother Question: {0}", anotherQuestion);

            Analytics.Client.Track(userId, "Chat Opened", new Properties() {
                { "source_brand", brandPlaceHolder }, // string brand where event is being tracked from
                { "category", "Chat" }, // string this will get mapped as a Google Analytics Event Category
                { "label", "Chat Opened" }, // string this will get mapped as a Google Analytics Event Label
                { "preferred_language", preferredLanguage }, // string user's preferred language
                { "user_question", userQuestion }, // string what is the user's question
                { "recommended_issue", recommenededIssue }, // string/int chat's recommened issue (if we have issues' ids we can track that for better understanding of chat's recommendation accuracy)
                { "is_question_recommended_issue", isQuestionRecommenedIssue }, // boolean is the chat's recommended issue the user's question
                { "another_question", anotherQuestion }, // boolean does the user have another question
            });
        }

        public static void PrintSummary()
        {
            Console.WriteLine($"Submitted: {Analytics.Client.Statistics.Submitted}");
            Console.WriteLine($"Failed: {Analytics.Client.Statistics.Failed}");
            Console.WriteLine($"Succeeded: {Analytics.Client.Statistics.Succeeded}");

        }

        public static class DotEnv
        {
            public static void Load(string filePath)
            {
                if (!File.Exists(filePath))
                    return;

                foreach (var line in File.ReadAllLines(filePath))
                {
                    var parts = line.Split(
                        '=',
                        StringSplitOptions.RemoveEmptyEntries);

                    if (parts.Length != 2)
                        continue;

                    Environment.SetEnvironmentVariable(parts[0], parts[1]);
                }
            }
        }
    }
}
