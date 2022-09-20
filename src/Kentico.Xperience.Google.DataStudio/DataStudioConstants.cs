using CMS.Activities;
using CMS.Automation;
using CMS.ContactManagement;
using CMS.DataProtection;
using CMS.Ecommerce;
using CMS.Globalization;
using CMS.Membership;
using CMS.Newsletters;
using CMS.SiteProvider;
using CMS.WorkflowEngine;

using Kentico.Xperience.Google.DataStudio.Models;
using Kentico.Xperience.Google.DataStudio.Services;

namespace Kentico.Xperience.Google.DataStudio
{
    /// <summary>
    /// Contains constants and pre-defined <see cref="FieldSet"/>s which may be referenced in an implementation
    /// of <see cref="IDataStudioFieldSetProvider"/> to determine the fields of the report.
    /// </summary>
    public static class DataStudioConstants
    {
        /// <summary>
        /// The application's relative path of the directory which contains the Google Data Studio report.
        /// </summary>
        public const string REPORT_DIRECTORY = "App_Data\\CMSModules\\Kentico.Xperience.Google.DataStudio";


        /// <summary>
        /// The filename of the Google Data Studio report.
        /// </summary>
        public const string REPORT_NAME = "datastudio.json";


        /// <summary>
        /// The settings key which stores the ID of the consent to check when generating the report.
        /// </summary>
        public const string SETTINGKEY_CONSENTID = "DataStudioConsentID";


        /// <summary>
        /// The default activity <see cref="FieldSet"/>.
        /// </summary>
        public static readonly FieldSet activityFieldSet = new FieldSet
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


        /// <summary>
        /// The default marketing automation state <see cref="FieldSet"/>.
        /// </summary>
        public static readonly FieldSet automationStateFieldSet = new FieldSet
        {
            ObjectType = AutomationStateInfo.OBJECT_TYPE,
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


        /// <summary>
        /// The default newsletter clicked link <see cref="FieldSet"/>.
        /// </summary>
        public static readonly FieldSet clickedLinkFieldSet = new FieldSet
        {
            ObjectType = ClickedLinkInfo.OBJECT_TYPE,
            DateFilterField = nameof(ClickedLinkInfo.ClickedLinkTime),
            Fields = new FieldDefinition[]
            {
                new FieldDefinition
                {
                    Name = nameof(ClickedLinkInfo.ClickedLinkID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                },
                new FieldDefinition {
                    Name = nameof(ClickedLinkInfo.ClickedLinkTime),
                    DataType = DataStudioFieldType.YEAR_MONTH_DAY_SECOND
                },
                new FieldDefinition {
                    Name = nameof(ClickedLinkInfo.ClickedLinkNewsletterLinkID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                }
            }
        };


        /// <summary>
        /// The default data protection consent agreement <see cref="FieldSet"/>.
        /// </summary>
        public static readonly FieldSet consentAgreementFieldSet = new FieldSet
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


        /// <summary>
        /// The default data protection consent <see cref="FieldSet"/>.
        /// </summary>
        public static readonly FieldSet consentFieldSet = new FieldSet
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


        /// <summary>
        /// The default contact <see cref="FieldSet"/>.
        /// </summary>
        public static readonly FieldSet contactFieldSet = new FieldSet
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
                    Name = nameof(ContactInfo.ContactCreated),
                    DataType = DataStudioFieldType.YEAR_MONTH_DAY_SECOND
                }
            }
        };


        /// <summary>
        /// The default contact group <see cref="FieldSet"/>.
        /// </summary>
        public static readonly FieldSet contactGroupFieldSet = new FieldSet
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


        /// <summary>
        /// The default contact group member <see cref="FieldSet"/>.
        /// </summary>
        public static readonly FieldSet contactGroupMemberFieldSet = new FieldSet
        {
            ObjectType = ContactGroupMemberInfo.OBJECT_TYPE_CONTACT,
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


        /// <summary>
        /// The default globalization country <see cref="FieldSet"/>.
        /// </summary>
        public static readonly FieldSet countryFieldSet = new FieldSet
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


        /// <summary>
        /// The default e-commerce customer <see cref="FieldSet"/>.
        /// </summary>
        public static readonly FieldSet customerFieldSet = new FieldSet
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


        /// <summary>
        /// The default e-commerce order <see cref="FieldSet"/>.
        /// </summary>
        public static readonly FieldSet orderFieldSet = new FieldSet
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


        /// <summary>
        /// The default newsletter issue <see cref="FieldSet"/>.
        /// </summary>
        public static readonly FieldSet newsletterIssueFieldSet = new FieldSet
        {
            ObjectType = IssueInfo.OBJECT_TYPE,
            DateFilterField = nameof(IssueInfo.IssueMailoutTime),
            Fields = new FieldDefinition[]
            {
                new FieldDefinition
                {
                    Name = nameof(IssueInfo.IssueID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                },
                new FieldDefinition
                {
                    Name = nameof(IssueInfo.IssueSiteID),
                    DataType = DataStudioFieldType.NUMBER,
                    Anonymize = true
                },
                new FieldDefinition
                {
                    Name = nameof(IssueInfo.IssueMailoutTime),
                    DataType = DataStudioFieldType.YEAR_MONTH_DAY_SECOND
                },
                new FieldDefinition
                {
                    Name = nameof(IssueInfo.IssueStatus),
                    DataType = DataStudioFieldType.NUMBER
                },
                new FieldDefinition
                {
                    Name = nameof(IssueInfo.IssueSubject),
                    DataType = DataStudioFieldType.TEXT
                },
                new FieldDefinition
                {
                    Name = nameof(IssueInfo.IssueSentEmails),
                    DataType = DataStudioFieldType.NUMBER
                },
                new FieldDefinition
                {
                    Name = nameof(IssueInfo.IssueBounces),
                    DataType = DataStudioFieldType.NUMBER
                },
                new FieldDefinition
                {
                    Name = nameof(IssueInfo.IssueOpenedEmails),
                    DataType = DataStudioFieldType.NUMBER
                },
                new FieldDefinition
                {
                    Name = nameof(IssueInfo.IssueUnsubscribed),
                    DataType = DataStudioFieldType.NUMBER
                }
            }
        };


        /// <summary>
        /// The default newsletter link <see cref="FieldSet"/>.
        /// </summary>
        public static readonly FieldSet newsletterLinkFieldSet = new FieldSet
        {
            ObjectType = LinkInfo.OBJECT_TYPE,
            Fields = new FieldDefinition[]
            {
                new FieldDefinition
                {
                    Name = nameof(LinkInfo.LinkID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                },
                new FieldDefinition
                {
                    Name = nameof(LinkInfo.LinkIssueID),
                    DataType = DataStudioFieldType.TEXT,
                    Anonymize = true
                },
            }
        };


        /// <summary>
        /// The default contact scoring rule <see cref="FieldSet"/>.
        /// </summary>
        public static readonly FieldSet ruleFieldSet = new FieldSet
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


        /// <summary>
        /// The default contact scoring contact rule <see cref="FieldSet"/>.
        /// </summary>
        public static readonly FieldSet scoreContactRuleFieldSet = new FieldSet
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


        /// <summary>
        /// The default contact scoring score <see cref="FieldSet"/>.
        /// </summary>
        public static readonly FieldSet scoreFieldSet = new FieldSet
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


        /// <summary>
        /// The default site <see cref="FieldSet"/>.
        /// </summary>
        public static readonly FieldSet siteFieldSet = new FieldSet
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


        /// <summary>
        /// The default globalization state <see cref="FieldSet"/>.
        /// </summary>
        public static readonly FieldSet stateFieldSet = new FieldSet
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


        /// <summary>
        /// The default user <see cref="FieldSet"/>.
        /// </summary>
        public static readonly FieldSet userFieldSet = new FieldSet
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
                    Name = nameof(UserInfo.UserCreated),
                    DataType = DataStudioFieldType.YEAR_MONTH_DAY_SECOND
                }
            }
        };


        /// <summary>
        /// The default workflow <see cref="FieldSet"/>.
        /// </summary>
        public static readonly FieldSet workflowFieldSet = new FieldSet
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
