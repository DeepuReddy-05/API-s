Public Class Service_ticket

    'TAB_SERVICE_TICKET
    'Start
    Public Property SERVICE_TICKET_NO() As String
        Get
            Return m_SERVICE_TICKET_NO
        End Get
        Set(value As String)
            m_SERVICE_TICKET_NO = value
        End Set
    End Property
    Private m_SERVICE_TICKET_NO As String

    Public Property CUT_HRS() As String
        Get
            Return m_CUT_HRS
        End Get
        Set(value As String)
            m_CUT_HRS = value
        End Set
    End Property
    Private m_CUT_HRS As String

    Public Property OPERATING_HRS() As String
        Get
            Return m_OPERATING_HRS
        End Get
        Set(value As String)
            m_OPERATING_HRS = value
        End Set
    End Property
    Private m_OPERATING_HRS As String

    Public Property POWER_ON_HRS() As String
        Get
            Return m_POWER_ON_HRS
        End Get
        Set(value As String)
            m_POWER_ON_HRS = value
        End Set
    End Property
    Private m_POWER_ON_HRS As String

    Public Property SERVICE_PERFORMED() As String
        Get
            Return m_SERVICE_PERFORMED
        End Get
        Set(value As String)
            m_SERVICE_PERFORMED = value
        End Set
    End Property
    Private m_SERVICE_PERFORMED As String


    Public Property PROBLEM_DESCRIPTION() As String
        Get
            Return m_PROBLEM_DESCRIPTION
        End Get
        Set(value As String)
            m_PROBLEM_DESCRIPTION = value
        End Set
    End Property
    Private m_PROBLEM_DESCRIPTION As String

    Public Property FOLLOWUP_REQUIRED() As String
        Get
            Return m_FOLLOWUP_REQUIRED
        End Get
        Set(value As String)
            m_FOLLOWUP_REQUIRED = value
        End Set
    End Property
    Private m_FOLLOWUP_REQUIRED As String


    'added Changes on 6 FEB 2020
    'Public Property TRAVEL_HOURS() As String
    '    Get
    '        Return m_TRAVEL_HOURS
    '    End Get
    '    Set(value As String)
    '        m_TRAVEL_HOURS = value
    '    End Set
    'End Property
    'Private m_TRAVEL_HOURS As String
    'ended changes on 6 FEB 2020

    Public Property USER_ID() As String
        Get
            Return m_USER_ID
        End Get
        Set(value As String)
            m_USER_ID = value
        End Set
    End Property
    Private m_USER_ID As String
    Public Property DEVICE_NAME() As String
        Get
            Return m_DEVICE_NAME
        End Get
        Set(value As String)
            m_DEVICE_NAME = value
        End Set
    End Property
    Private m_DEVICE_NAME As String
    'End


    'TAB_SERVICE_TICKET_TIME
    'Start
    Public Property RECORD_NO() As String
        Get
            Return m_RECORD_NO
        End Get
        Set(value As String)
            m_RECORD_NO = value
        End Set
    End Property
    Private m_RECORD_NO As String

    Public Property ENTRY_DATE() As String
        Get
            Return m_ENTRY_DATE
        End Get
        Set(value As String)
            m_ENTRY_DATE = value
        End Set
    End Property
    Private m_ENTRY_DATE As String

    Public Property TIME_IN_HRS() As String
        Get
            Return m_TIME_IN_HRS
        End Get
        Set(value As String)
            m_TIME_IN_HRS = value
        End Set
    End Property
    Private m_TIME_IN_HRS As String

    Public Property TIME_IN_MINS() As String
        Get
            Return m_TIME_IN_MINS
        End Get
        Set(value As String)
            m_TIME_IN_MINS = value
        End Set
    End Property
    Private m_TIME_IN_MINS As String


    Public Property TIME_OUT_HRS() As String
        Get
            Return m_TIME_OUT_HRS
        End Get
        Set(value As String)
            m_TIME_OUT_HRS = value
        End Set
    End Property
    Private m_TIME_OUT_HRS As String

    Public Property TIME_OUT_MINS() As String
        Get
            Return m_TIME_OUT_MINS
        End Get
        Set(value As String)
            m_TIME_OUT_MINS = value
        End Set
    End Property
    Private m_TIME_OUT_MINS As String

    Public Property WORK_HOURS() As String
        Get
            Return m_WORK_HOURS
        End Get
        Set(value As String)
            m_WORK_HOURS = value
        End Set
    End Property
    Private m_WORK_HOURS As String

    Public Property TRAVEL_HOURS() As String
        Get
            Return m_TRAVEL_HOURS
        End Get
        Set(value As String)
            m_TRAVEL_HOURS = value
        End Set
    End Property
    Private m_TRAVEL_HOURS As String

    Public Property LUNCH_HOURS() As String
        Get
            Return m_LUNCH_HOURS
        End Get
        Set(value As String)
            m_LUNCH_HOURS = value
        End Set
    End Property
    Private m_LUNCH_HOURS As String

    Public Property SERVICE_PERSON_ID() As String
        Get
            Return m_SERVICE_PERSON_ID
        End Get
        Set(value As String)
            m_SERVICE_PERSON_ID = value
        End Set
    End Property
    Private m_SERVICE_PERSON_ID As String

    Public Property BILLABLE() As String
        Get
            Return m_BILLABLE
        End Get
        Set(value As String)
            m_BILLABLE = value
        End Set
    End Property
    Private m_BILLABLE As String

    Public Property BILLED() As String
        Get
            Return m_BILLED
        End Get
        Set(value As String)
            m_BILLED = value
        End Set
    End Property
    Private m_BILLED As String

    Public Property SIGNATURE_PATH() As String
        Get
            Return m_SIGNATURE_PATH
        End Get
        Set(value As String)
            m_SIGNATURE_PATH = value
        End Set
    End Property
    Private m_SIGNATURE_PATH As String

    Public Property INTERNAL_LINE_NO() As String
        Get
            Return m_INTERNAL_LINE_NO
        End Get
        Set(value As String)
            m_INTERNAL_LINE_NO = value
        End Set
    End Property
    Private m_INTERNAL_LINE_NO As String
    'End

    'TAB_SERVICE_TICKET_EXPENSE
    'Start
    Public Property EXP_RECORD_NO() As String
        Get
            Return m_EXP_RECORD_NO
        End Get
        Set(value As String)
            m_EXP_RECORD_NO = value
        End Set
    End Property
    Private m_EXP_RECORD_NO As String

    Public Property EXP_ENTRY_DATE() As String
        Get
            Return m_EXP_ENTRY_DATE
        End Get
        Set(value As String)
            m_EXP_ENTRY_DATE = value
        End Set
    End Property
    Private m_EXP_ENTRY_DATE As String

    Public Property CATEGORY() As String
        Get
            Return m_CATEGORY
        End Get
        Set(value As String)
            m_CATEGORY = value
        End Set
    End Property
    Private m_CATEGORY As String

    Public Property BILLED_AMOUNT() As String
        Get
            Return m_BILLED_AMOUNT
        End Get
        Set(value As String)
            m_BILLED_AMOUNT = value
        End Set
    End Property
    Private m_BILLED_AMOUNT As String

    Public Property EXP_SERVICE_PERSON_ID() As String
        Get
            Return m_EXP_SERVICE_PERSON_ID
        End Get
        Set(value As String)
            m_EXP_SERVICE_PERSON_ID = value
        End Set
    End Property
    Private m_EXP_SERVICE_PERSON_ID As String

    Public Property EXP_BILLABLE() As String
        Get
            Return m_EXP_BILLABLE
        End Get
        Set(value As String)
            m_EXP_BILLABLE = value
        End Set
    End Property
    Private m_EXP_BILLABLE As String

    Public Property EXP_BILLED() As String
        Get
            Return m_EXP_BILLED
        End Get
        Set(value As String)
            m_EXP_BILLED = value
        End Set
    End Property
    Private m_EXP_BILLED As String

    Public Property EXP_INTERNAL_LINE_NO() As String
        Get
            Return m_EXP_INTERNAL_LINE_NO
        End Get
        Set(value As String)
            m_EXP_INTERNAL_LINE_NO = value
        End Set
    End Property
    Private m_EXP_INTERNAL_LINE_NO As String
    'End

End Class
