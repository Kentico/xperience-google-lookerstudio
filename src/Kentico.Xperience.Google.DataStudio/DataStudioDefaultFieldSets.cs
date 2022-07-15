using CMS.Activities;
using CMS.Automation;
using CMS.ContactManagement;
using CMS.DataProtection;
using CMS.Ecommerce;
using CMS.Globalization;
using CMS.Membership;
using CMS.SiteProvider;
using CMS.WorkflowEngine;

using Kentico.Xperience.Google.DataStudio.Models;

namespace Kentico.Xperience.Google.DataStudio
{
    public class DataStudioDefaultFieldSets
    {
        public static FieldSet activityFieldSet = new FieldSet
        {
            ObjectType = ActivityInfo.OBJECT_TYPE,
            DateFilterField = nameof(ActivityInfo.ActivityCreated),
            Fields = new FieldDefinition[] {
                new FieldDefinition
                {
                    Name = nameof(ActivityInfo.ActivityID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                },
                new FieldDefinition
                {
                    Name = nameof(ActivityInfo.ActivityTitle),
                    DataType = DataStudioFieldType.TEXT
                },
                new FieldDefinition
                {
                    Name = nameof(ActivityInfo.ActivityURL),
                    DataType = DataStudioFieldType.URL
                },
                new FieldDefinition
                {
                    Name = nameof(ActivityInfo.ActivityCampaign),
                    DataType = DataStudioFieldType.TEXT
                },
                new FieldDefinition
                {
                    Name = nameof(ActivityInfo.ActivityUTMSource),
                    DataType = DataStudioFieldType.TEXT
                },
                new FieldDefinition
                {
                    Name = nameof(ActivityInfo.ActivityUTMContent),
                    DataType = DataStudioFieldType.TEXT
                },
                new FieldDefinition
                {
                    Name = nameof(ActivityInfo.ActivityContactID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                },
                new FieldDefinition
                {
                    Name = nameof(ActivityInfo.ActivityCreated),
                    DataType = DataStudioFieldType.YEAR_MONTH_DAY_SECOND
                },
                new FieldDefinition
                {
                    Name = nameof(ActivityInfo.ActivityType),
                    DataType = DataStudioFieldType.TEXT
                }
            }
        };


        public static FieldSet automationStateFieldSet = new FieldSet
        {
            ObjectType = "cms.automationstate",
            DateFilterField = nameof(AutomationStateInfo.StateCreated),
            Fields = new FieldDefinition[]
            {
                new FieldDefinition {
                    Name = nameof(AutomationStateInfo.StateCreated),
                    DataType = DataStudioFieldType.YEAR_MONTH_DAY_SECOND
                },
                new FieldDefinition {
                    Name = nameof(AutomationStateInfo.StateID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                },
                new FieldDefinition {
                    Name = nameof(AutomationStateInfo.StateStepID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                },
                new FieldDefinition {
                    Name = nameof(AutomationStateInfo.StateObjectID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                },
                new FieldDefinition {
                    Name = nameof(AutomationStateInfo.StateObjectType),
                    DataType = DataStudioFieldType.TEXT
                },
                new FieldDefinition {
                    Name = nameof(AutomationStateInfo.StateWorkflowID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                },
                new FieldDefinition {
                    Name = nameof(AutomationStateInfo.StateStatus),
                    DataType = DataStudioFieldType.NUMBER
                },
                new FieldDefinition {
                    Name = nameof(AutomationStateInfo.StateSiteID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                }
            }
        };


        public static FieldSet consentAgreementFieldSet = new FieldSet
        {
            ObjectType = ConsentAgreementInfo.OBJECT_TYPE,
            DateFilterField = nameof(ConsentAgreementInfo.ConsentAgreementTime),
            Fields = new FieldDefinition[]
            {
                new FieldDefinition {
                    Name = nameof(ConsentAgreementInfo.ConsentAgreementTime),
                    DataType = DataStudioFieldType.YEAR_MONTH_DAY_SECOND
                },
                new FieldDefinition {
                    Name = nameof(ConsentAgreementInfo.ConsentAgreementRevoked),
                    DataType = DataStudioFieldType.BOOLEAN
                },
                new FieldDefinition {
                    Name = nameof(ConsentAgreementInfo.ConsentAgreementContactID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                },
                new FieldDefinition {
                    Name = nameof(ConsentAgreementInfo.ConsentAgreementConsentID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                }
            }
        };


        public static FieldSet consentFieldSet = new FieldSet
        {
            ObjectType = ConsentInfo.OBJECT_TYPE,
            Fields = new FieldDefinition[]
            {
                new FieldDefinition {
                    Name = nameof(ConsentInfo.ConsentID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                },
                new FieldDefinition {
                    Name = nameof(ConsentInfo.ConsentLastModified),
                    DataType = DataStudioFieldType.YEAR_MONTH_DAY_SECOND
                },
                new FieldDefinition {
                    Name = nameof(ConsentInfo.ConsentDisplayName),
                    DataType = DataStudioFieldType.TEXT
                }
            }
        };


        public static FieldSet contactFieldSet = new FieldSet
        {
            ObjectType = ContactInfo.OBJECT_TYPE,
            DateFilterField = nameof(ContactInfo.ContactCreated),
            Fields = new FieldDefinition[]
            {
                new FieldDefinition {
                    Name = nameof(ContactInfo.ContactID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                },
                new FieldDefinition {
                    Name = nameof(ContactInfo.ContactFirstName),
                    DataType = DataStudioFieldType.TEXT
                },
                new FieldDefinition {
                    Name = nameof(ContactInfo.ContactMiddleName),
                    DataType = DataStudioFieldType.TEXT
                },
                new FieldDefinition {
                    Name = nameof(ContactInfo.ContactLastName),
                    DataType = DataStudioFieldType.TEXT
                },
                new FieldDefinition {
                    Name = nameof(ContactInfo.ContactJobTitle),
                    DataType = DataStudioFieldType.TEXT
                },
                new FieldDefinition {
                    Name = nameof(ContactInfo.ContactCompanyName),
                    DataType = DataStudioFieldType.TEXT
                },
                new FieldDefinition {
                    Name = nameof(ContactInfo.ContactAddress1),
                    DataType = DataStudioFieldType.TEXT
                },
                new FieldDefinition {
                    Name = nameof(ContactInfo.ContactCity),
                    DataType = DataStudioFieldType.CITY
                },
                new FieldDefinition {
                    Name = nameof(ContactInfo.ContactZIP),
                    DataType = DataStudioFieldType.TEXT
                },
                new FieldDefinition {
                    Name = nameof(ContactInfo.ContactCountryID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                },
                new FieldDefinition {
                    Name = nameof(ContactInfo.ContactStateID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                },
                new FieldDefinition {
                    Name = nameof(ContactInfo.ContactEmail),
                    DataType = DataStudioFieldType.TEXT
                },
                new FieldDefinition {
                    Name = nameof(ContactInfo.ContactCreated),
                    DataType = DataStudioFieldType.YEAR_MONTH_DAY_SECOND
                }
            }
        };


        public static FieldSet contactGroupFieldSet = new FieldSet
        {
            ObjectType = ContactGroupInfo.OBJECT_TYPE,
            DateFilterField = nameof(ContactGroupInfo.ContactGroupLastModified),
            Fields = new FieldDefinition[]
            {
                new FieldDefinition {
                    Name = nameof(ContactGroupInfo.ContactGroupLastModified),
                    DataType = DataStudioFieldType.YEAR_MONTH_DAY_SECOND
                },
                new FieldDefinition {
                    Name = nameof(ContactGroupInfo.ContactGroupDisplayName),
                    DataType = DataStudioFieldType.TEXT
                },
                new FieldDefinition {
                    Name = nameof(ContactGroupInfo.ContactGroupID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                }
            }
        };


        public static FieldSet contactGroupMemberFieldSet = new FieldSet
        {
            ObjectType = "om.contactgroupmember",
            Fields = new FieldDefinition[]
            {
                new FieldDefinition
                {
                    Name = nameof(ContactGroupMemberInfo.ContactGroupMemberContactGroupID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                },
                new FieldDefinition
                {
                    Name = nameof(ContactGroupMemberInfo.ContactGroupMemberRelatedID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                }
            }
        };


        public static FieldSet countryFieldSet = new FieldSet
        {
            ObjectType = CountryInfo.OBJECT_TYPE,
            Fields = new FieldDefinition[]
            {
                new FieldDefinition
                {
                    Name = nameof(CountryInfo.CountryID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                },
                new FieldDefinition
                {
                    Name = nameof(CountryInfo.CountryDisplayName),
                    DataType = DataStudioFieldType.COUNTRY
                },
                new FieldDefinition
                {
                    Name = nameof(CountryInfo.CountryTwoLetterCode),
                    DataType = DataStudioFieldType.COUNTRY_CODE
                }
            }
        };


        public static FieldSet customerFieldSet = new FieldSet
        {
            ObjectType = CustomerInfo.OBJECT_TYPE,
            DateFilterField = nameof(CustomerInfo.CustomerCreated),
            Fields = new FieldDefinition[]
            {
                new FieldDefinition
                {
                    Name = nameof(CustomerInfo.CustomerID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                },
                new FieldDefinition
                {
                    Name = nameof(CustomerInfo.CustomerFirstName),
                    DataType = DataStudioFieldType.TEXT
                },
                new FieldDefinition
                {
                    Name = nameof(CustomerInfo.CustomerLastName),
                    DataType = DataStudioFieldType.TEXT
                },
                new FieldDefinition
                {
                    Name = nameof(CustomerInfo.CustomerEmail),
                    DataType = DataStudioFieldType.TEXT
                },
                new FieldDefinition
                {
                    Name = nameof(CustomerInfo.CustomerPhone),
                    DataType = DataStudioFieldType.TEXT
                },
                new FieldDefinition
                {
                    Name = nameof(CustomerInfo.CustomerUserID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                },
                new FieldDefinition
                {
                    Name = nameof(CustomerInfo.CustomerSiteID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                },
                new FieldDefinition
                {
                    Name = nameof(CustomerInfo.CustomerCreated),
                    DataType = DataStudioFieldType.YEAR_MONTH_DAY_SECOND
                }
            }
        };


        public static FieldSet orderFieldSet = new FieldSet
        {
            ObjectType = OrderInfo.OBJECT_TYPE,
            DateFilterField = nameof(OrderInfo.OrderDate),
            Fields = new FieldDefinition[]
            {
                new FieldDefinition
                {
                    Name = nameof(OrderInfo.OrderID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                },
                new FieldDefinition
                {
                    Name = nameof(OrderInfo.OrderDate),
                    DataType = DataStudioFieldType.YEAR_MONTH_DAY_SECOND
                },
                new FieldDefinition
                {
                    Name = nameof(OrderInfo.OrderCustomerID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                },
                new FieldDefinition
                {
                    Name = nameof(OrderInfo.OrderSiteID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                },
                new FieldDefinition
                {
                    Name = nameof(OrderInfo.OrderIsPaid),
                    DataType = DataStudioFieldType.BOOLEAN
                },
                new FieldDefinition
                {
                    Name = nameof(OrderInfo.OrderTotalPrice),
                    DataType = DataStudioFieldType.NUMBER
                },
                new FieldDefinition
                {
                    Name = nameof(OrderInfo.OrderTotalShipping),
                    DataType = DataStudioFieldType.NUMBER
                },
                new FieldDefinition
                {
                    Name = nameof(OrderInfo.OrderTotalTax),
                    DataType = DataStudioFieldType.NUMBER
                }
            }
        };


        public static FieldSet ruleFieldSet = new FieldSet
        {
            ObjectType = RuleInfo.OBJECT_TYPE,
            Fields = new FieldDefinition[]
            {
                new FieldDefinition
                {
                    Name = nameof(RuleInfo.RuleID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                },
                new FieldDefinition
                {
                    Name = nameof(RuleInfo.RuleScoreID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                },
                new FieldDefinition
                {
                    Name = nameof(RuleInfo.RuleDisplayName),
                    DataType = DataStudioFieldType.TEXT
                },
                new FieldDefinition
                {
                    Name = nameof(RuleInfo.RuleValue),
                    DataType = DataStudioFieldType.NUMBER
                },
                new FieldDefinition
                {
                    Name = nameof(RuleInfo.RuleMaxPoints),
                    DataType = DataStudioFieldType.NUMBER
                },
                new FieldDefinition
                {
                    Name = nameof(RuleInfo.RuleValidUntil),
                    DataType = DataStudioFieldType.YEAR_MONTH_DAY_SECOND
                },
                new FieldDefinition
                {
                    Name = nameof(RuleInfo.RuleValidity),
                    DataType = DataStudioFieldType.NUMBER
                },
                new FieldDefinition
                {
                    Name = nameof(RuleInfo.RuleValidFor),
                    DataType = DataStudioFieldType.NUMBER
                },
                new FieldDefinition
                {
                    Name = nameof(RuleInfo.RuleIsRecurring),
                    DataType = DataStudioFieldType.BOOLEAN
                },
                new FieldDefinition
                {
                    Name = nameof(RuleInfo.RuleType),
                    DataType = DataStudioFieldType.NUMBER
                },
                new FieldDefinition
                {
                    Name = nameof(RuleInfo.RuleParameter),
                    DataType = DataStudioFieldType.TEXT
                },
            }
        };


        public static FieldSet scoreContactRuleFieldSet = new FieldSet
        {
            ObjectType = ScoreContactRuleInfo.OBJECT_TYPE,
            Fields = new FieldDefinition[]
            {
                new FieldDefinition
                {
                    Name = nameof(ScoreContactRuleInfo.ScoreID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                },
                new FieldDefinition
                {
                    Name = nameof(ScoreContactRuleInfo.ContactID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                },
                new FieldDefinition
                {
                    Name = nameof(ScoreContactRuleInfo.RuleID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                },
                new FieldDefinition
                {
                    Name = nameof(ScoreContactRuleInfo.Value),
                    DataType = DataStudioFieldType.NUMBER
                }
            }
        };


        public static FieldSet scoreFieldSet = new FieldSet
        {
            ObjectType = ScoreInfo.OBJECT_TYPE,
            Fields = new FieldDefinition[]
            {
                new FieldDefinition
                {
                    Name = nameof(ScoreInfo.ScoreID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                },
                new FieldDefinition
                {
                    Name = nameof(ScoreInfo.ScoreDisplayName),
                    DataType = DataStudioFieldType.TEXT
                },
                new FieldDefinition
                {
                    Name = nameof(ScoreInfo.ScoreEnabled),
                    DataType = DataStudioFieldType.BOOLEAN
                },
                new FieldDefinition
                {
                    Name = nameof(ScoreInfo.ScoreStatus),
                    DataType = DataStudioFieldType.NUMBER
                }
            }
        };


        public static FieldSet siteFieldSet = new FieldSet
        {
            ObjectType = SiteInfo.OBJECT_TYPE,
            Fields = new FieldDefinition[]
            {
                new FieldDefinition
                {
                    Name = nameof(SiteInfo.SiteID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                },
                new FieldDefinition
                {
                    Name = nameof(SiteInfo.SiteName),
                    DataType = DataStudioFieldType.TEXT
                }
            }
        };


        public static FieldSet stateFieldSet = new FieldSet
        {
            ObjectType = StateInfo.OBJECT_TYPE,
            Fields = new FieldDefinition[]
            {
                new FieldDefinition
                {
                    Name = nameof(StateInfo.StateID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                },
                new FieldDefinition
                {
                    Name = nameof(StateInfo.StateDisplayName),
                    DataType = DataStudioFieldType.TEXT
                }
            }
        };


        public static FieldSet userFieldSet = new FieldSet
        {
            ObjectType = UserInfo.OBJECT_TYPE,
            DateFilterField = nameof(UserInfo.UserCreated),
            Fields = new FieldDefinition[]
            {
                new FieldDefinition
                {
                    Name = nameof(UserInfo.UserID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                },
                new FieldDefinition
                {
                    Name = nameof(UserInfo.UserName),
                    DataType = DataStudioFieldType.TEXT
                },
                new FieldDefinition
                {
                    Name = nameof(UserInfo.FullName),
                    DataType = DataStudioFieldType.TEXT
                },
                new FieldDefinition
                {
                    Name = nameof(UserInfo.Email),
                    DataType = DataStudioFieldType.TEXT
                },
                new FieldDefinition
                {
                    Name = nameof(UserInfo.UserCreated),
                    DataType = DataStudioFieldType.YEAR_MONTH_DAY_SECOND
                }
            }
        };


        public static FieldSet workflowFieldSet = new FieldSet
        {
            ObjectType = WorkflowInfo.OBJECT_TYPE,
            Fields = new FieldDefinition[]
            {
                new FieldDefinition
                {
                    Name = nameof(WorkflowInfo.WorkflowID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                },
                new FieldDefinition
                {
                    Name = nameof(WorkflowInfo.WorkflowDisplayName),
                    DataType = DataStudioFieldType.TEXT
                },
                new FieldDefinition
                {
                    Name = nameof(WorkflowInfo.WorkflowType),
                    DataType = DataStudioFieldType.NUMBER
                },
                new FieldDefinition
                {
                    Name = nameof(WorkflowInfo.WorkflowEnabled),
                    DataType = DataStudioFieldType.BOOLEAN
                }
            }
        };
    }
}
