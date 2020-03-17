Imports System.Data.OleDb
Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports System.Xml
Imports Newtonsoft.Json

Namespace Controllers
    Public Class UpdateServiceTicketsController
        Inherits ApiController

        Protected dbad As New OleDbDataAdapter
        Dim Connection As [String] = ConfigurationManager.AppSettings("dbconn.ConnectionString")
        Protected Shared AppPath As String = System.AppDomain.CurrentDomain.BaseDirectory
        Public dbadc As New OleDbCommand
        Dim con As New OleDbConnection(Connection)
        Dim cmd As New OleDbCommand()


        Public Function Post(request As HttpRequestMessage) As HttpResponseMessage
            Dim result As String = String.Empty
            Dim resp = New HttpResponseMessage(HttpStatusCode.OK)
            dbad.SelectCommand = New OleDbCommand
            dbad.UpdateCommand = New OleDbCommand
            Dim res As Int32 = 0

            Dim xd As New XmlDocument()

            Dim filetype As String = request.Content.Headers.ContentType.ToString()
            If filetype.Contains("json") Then
                xd = JsonConvert.DeserializeXmlNode(request.Content.ReadAsStringAsync().Result)
            Else
                xd.Load(request.Content.ReadAsStreamAsync().Result)
            End If


            Dim path As String = AppPath + "\" + "XML\"
            If Not Directory.Exists(path) Then
                Directory.CreateDirectory(path)
            End If
            xd.Save(path & DateTime.Now.ToString("yyyyMMddHHmmss") & ".xml")


            Dim nodelist As XmlNodeList = xd.SelectNodes("/ArrayOfTickets/SERVICE_TICKET")

            ' get all <Service Ticket> nodes
            For Each node As XmlNode In nodelist
                ' for each <Service Ticket> node
                Dim Service_ticket As New Service_ticket()
                Try
                    'TAB_SERVICE_TICKET
                    'Start

                    If Not node.SelectSingleNode("SERVICE_TICKET_NO") Is Nothing Then
                        Service_ticket.SERVICE_TICKET_NO = node.SelectSingleNode("SERVICE_TICKET_NO").InnerText
                    End If

                    If Not node.SelectSingleNode("CUT_HRS") Is Nothing Then
                        Service_ticket.CUT_HRS = node.SelectSingleNode("CUT_HRS").InnerText
                    End If

                    If Not node.SelectSingleNode("OPERATING_HRS") Is Nothing Then
                        Service_ticket.OPERATING_HRS = node.SelectSingleNode("OPERATING_HRS").InnerText
                    End If

                    If Not node.SelectSingleNode("POWER_ON_HRS") Is Nothing Then
                        Service_ticket.POWER_ON_HRS = node.SelectSingleNode("POWER_ON_HRS").InnerText
                    End If

                    If Not node.SelectSingleNode("SERVICE_PERFORMED") Is Nothing Then
                        Service_ticket.SERVICE_PERFORMED = Replace(node.SelectSingleNode("SERVICE_PERFORMED").InnerText, "'", "''")
                    End If

                    If Not node.SelectSingleNode("PROBLEM_DESCRIPTION") Is Nothing Then
                        Service_ticket.PROBLEM_DESCRIPTION = Replace(node.SelectSingleNode("PROBLEM_DESCRIPTION").InnerText, "'", "''")
                    End If

                    If Not node.SelectSingleNode("FOLLOWUP_REQUIRED") Is Nothing Then
                        Service_ticket.FOLLOWUP_REQUIRED = node.SelectSingleNode("FOLLOWUP_REQUIRED").InnerText
                    End If
                    If Not node.SelectSingleNode("USER_ID") Is Nothing Then
                        Service_ticket.USER_ID = node.SelectSingleNode("USER_ID").InnerText
                    End If
                    If Not node.SelectSingleNode("DEVICE_NAME") Is Nothing Then
                        Service_ticket.DEVICE_NAME = node.SelectSingleNode("DEVICE_NAME").InnerText
                    End If

                    Dim ds_new As New System.Data.DataSet
                    con.Open()
                    dbad.SelectCommand.Connection = con
                    dbad.SelectCommand.CommandText = "SELECT * FROM TAB_SERVICE_TICKET  WHERE SERVICE_TICKET_NO='" + Service_ticket.SERVICE_TICKET_NO + "'"
                    dbad.Fill(ds_new)
                    dbad.SelectCommand.Connection.Close()
                    Dim strSQL As [String]

                    If (ds_new.Tables(0).Rows.Count > 0) Then
                        strSQL = "UPDATE TAB_SERVICE_TICKET SET USER_ID='" + Service_ticket.USER_ID + "',DEVICE_NAME='" + Service_ticket.DEVICE_NAME + "',CUT_HRS='" + Service_ticket.CUT_HRS + "',OPERATING_HRS='" + Service_ticket.OPERATING_HRS + "',POWER_ON_HRS='" + Service_ticket.POWER_ON_HRS + "',SERVICE_PERFORMED='" + Service_ticket.SERVICE_PERFORMED + "',PROBLEM_DESCRIPTION='" + Service_ticket.PROBLEM_DESCRIPTION + "',FOLLOWUP_REQUIRED='" + Service_ticket.FOLLOWUP_REQUIRED + "',PROCESS_FLAG='N' WHERE SERVICE_TICKET_NO='" + Service_ticket.SERVICE_TICKET_NO + "'"
                    Else
                        strSQL = "INSERT INTO TAB_SERVICE_TICKET (SERVICE_TICKET_NO,CUT_HRS,OPERATING_HRS,POWER_ON_HRS,SERVICE_PERFORMED,PROBLEM_DESCRIPTION,FOLLOWUP_REQUIRED,PROCESS_FLAG,USER_ID,DEVICE_NAME) VALUES ('" + Service_ticket.SERVICE_TICKET_NO + "','" + Service_ticket.CUT_HRS + "','" + Service_ticket.OPERATING_HRS + "','" + Service_ticket.POWER_ON_HRS + "','" + Service_ticket.SERVICE_PERFORMED + "','" + Service_ticket.PROBLEM_DESCRIPTION + "','" + Service_ticket.FOLLOWUP_REQUIRED + "','N','" + Service_ticket.USER_ID + "','" + Service_ticket.DEVICE_NAME + "')"
                    End If


                    con.Open()
                    cmd = New OleDbCommand(strSQL, con)
                    Try
                        res = cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        Call insert_sys_log("Exception while inserting", strSQL)
                    End Try

                    con.Close()
                    'End



                    'TAB_SERVICE_TICKET_TIME
                    'Start
                    Dim nodelistinner As XmlNodeList = node.SelectNodes("TICKET_TIME")  'xd.SelectNodes("/ArrayOfTickets/SERVICE_TICKET/TICKET_TIME")

                    For Each nodeinner As XmlNode In nodelistinner

                        If Not nodeinner.SelectSingleNode("RECORD_NO") Is Nothing Then
                            Service_ticket.RECORD_NO = nodeinner.SelectSingleNode("RECORD_NO").InnerText
                        End If

                        If Not nodeinner.SelectSingleNode("ENTRY_DATE") Is Nothing Then
                            Service_ticket.ENTRY_DATE = nodeinner.SelectSingleNode("ENTRY_DATE").InnerText
                        End If


                        If Not nodeinner.SelectSingleNode("TIME_IN_HRS") Is Nothing Then
                            Service_ticket.TIME_IN_HRS = nodeinner.SelectSingleNode("TIME_IN_HRS").InnerText
                        End If

                        If Not nodeinner.SelectSingleNode("TIME_IN_MINS") Is Nothing Then
                            Service_ticket.TIME_IN_MINS = nodeinner.SelectSingleNode("TIME_IN_MINS").InnerText
                        End If

                        If Not nodeinner.SelectSingleNode("TIME_OUT_HRS") Is Nothing Then
                            Service_ticket.TIME_OUT_HRS = nodeinner.SelectSingleNode("TIME_OUT_HRS").InnerText
                        End If

                        If Not nodeinner.SelectSingleNode("TIME_OUT_MINS") Is Nothing Then
                            Service_ticket.TIME_OUT_MINS = nodeinner.SelectSingleNode("TIME_OUT_MINS").InnerText
                        End If

                        If Not nodeinner.SelectSingleNode("WORK_HOURS") Is Nothing Then
                            Service_ticket.WORK_HOURS = nodeinner.SelectSingleNode("WORK_HOURS").InnerText
                        End If

                        If Not nodeinner.SelectSingleNode("TRAVEL_HOURS") Is Nothing Then
                            Service_ticket.TRAVEL_HOURS = nodeinner.SelectSingleNode("TRAVEL_HOURS").InnerText
                        End If

                        If Not nodeinner.SelectSingleNode("LUNCH_HOURS") Is Nothing Then
                            Service_ticket.LUNCH_HOURS = nodeinner.SelectSingleNode("LUNCH_HOURS").InnerText
                        End If

                        If Not nodeinner.SelectSingleNode("SERVICE_PERSON_ID") Is Nothing Then
                            Service_ticket.SERVICE_PERSON_ID = nodeinner.SelectSingleNode("SERVICE_PERSON_ID").InnerText
                        End If

                        If Not nodeinner.SelectSingleNode("BILLABLE") Is Nothing Then
                            Service_ticket.BILLABLE = nodeinner.SelectSingleNode("BILLABLE").InnerText
                        End If

                        If Not nodeinner.SelectSingleNode("BILLED") Is Nothing Then
                            Service_ticket.BILLED = nodeinner.SelectSingleNode("BILLED").InnerText
                        End If

                        If Not nodeinner.SelectSingleNode("SIGNATURE_PATH") Is Nothing Then
                            Service_ticket.SIGNATURE_PATH = nodeinner.SelectSingleNode("SIGNATURE_PATH").InnerText
                        End If

                        If Not nodeinner.SelectSingleNode("INTERNAL_LINE_NO") Is Nothing Then
                            Service_ticket.INTERNAL_LINE_NO = nodeinner.SelectSingleNode("INTERNAL_LINE_NO").InnerText
                        End If

                        Dim strSQL1 As [String]
                        If (Service_ticket.INTERNAL_LINE_NO <> "") Then
                            strSQL1 = "INSERT INTO TAB_SERVICE_TICKET_TIME (SERVICE_TICKET_NO,RECORD_NO,ENTRY_DATE,TIME_IN_HRS,TIME_IN_MINS,TIME_OUT_HRS,TIME_OUT_MINS,WORK_HOURS,TRAVEL_HOURS,LUNCH_HOURS,SERVICE_PERSON_ID,BILLABLE,BILLED,SIGNATURE_PATH,INTERNAL_LINE_NO) VALUES ('" + Service_ticket.SERVICE_TICKET_NO + "',(SELECT NVL(MAX(RECORD_NO),0)+1 FROM TAB_SERVICE_TICKET_TIME WHERE SERVICE_TICKET_NO='" + Service_ticket.SERVICE_TICKET_NO + "') ,TO_DATE('" + Service_ticket.ENTRY_DATE + "','MM/DD/RRRR'),'" + Service_ticket.TIME_IN_HRS + "','" + Service_ticket.TIME_IN_MINS + "','" + Service_ticket.TIME_OUT_HRS + "','" + Service_ticket.TIME_OUT_MINS + "','" + Service_ticket.WORK_HOURS + "','" + Service_ticket.TRAVEL_HOURS + "','" + Service_ticket.LUNCH_HOURS + "','" + Service_ticket.SERVICE_PERSON_ID + "','" + Service_ticket.BILLABLE + "','" + Service_ticket.BILLED + "','" + Service_ticket.SIGNATURE_PATH + "','" + Service_ticket.INTERNAL_LINE_NO + "')"
                        Else
                            strSQL1 = "UPDATE TAB_SERVICE_TICKET_TIME SET ENTRY_DATE=TO_DATE('" + Service_ticket.ENTRY_DATE + "','MM/DD/RRRR'),TIME_IN_HRS='" + Service_ticket.TIME_IN_HRS + "',TIME_IN_MINS='" + Service_ticket.TIME_IN_MINS + "',TIME_OUT_HRS='" + Service_ticket.TIME_OUT_HRS + "',TIME_OUT_MINS='" + Service_ticket.TIME_OUT_MINS + "',WORK_HOURS='" + Service_ticket.WORK_HOURS + "',TRAVEL_HOURS='" + Service_ticket.TRAVEL_HOURS + "',LUNCH_HOURS='" + Service_ticket.LUNCH_HOURS + "',SERVICE_PERSON_ID='" + Service_ticket.SERVICE_PERSON_ID + "',BILLABLE='" + Service_ticket.BILLABLE + "',BILLED='" + Service_ticket.BILLED + "',SIGNATURE_PATH='" + Service_ticket.SIGNATURE_PATH + "',INTERNAL_LINE_NO='" + Service_ticket.INTERNAL_LINE_NO + "'  WHERE SERVICE_TICKET_NO='" + Service_ticket.SERVICE_TICKET_NO + "' AND RECORD_NO='" + Service_ticket.RECORD_NO + "'"
                        End If
                        con.Open()
                        cmd = New OleDbCommand(strSQL1, con)
                        Try
                            res = cmd.ExecuteNonQuery()
                        Catch ex As Exception
                            Call insert_sys_log("Exception while inserting", strSQL1)
                        End Try

                        con.Close()
                        'End
                    Next

                    'TAB_SERVICE_TICKET_EXPENSE
                    'Start
                    Dim nodelistinner1 As XmlNodeList = node.SelectNodes("TICKET_EXPENSE") 'xd.SelectNodes("/ArrayOfTickets/SERVICE_TICKET/TICKET_EXPENSE")

                    For Each nodeinner As XmlNode In nodelistinner1

                        If Not nodeinner.SelectSingleNode("RECORD_NO") Is Nothing Then
                            Service_ticket.EXP_RECORD_NO = nodeinner.SelectSingleNode("RECORD_NO").InnerText
                        End If

                        If Not nodeinner.SelectSingleNode("ENTRY_DATE") Is Nothing Then
                            Service_ticket.EXP_ENTRY_DATE = nodeinner.SelectSingleNode("ENTRY_DATE").InnerText
                        End If

                        If Not nodeinner.SelectSingleNode("CATEGORY") Is Nothing Then
                            Service_ticket.CATEGORY = nodeinner.SelectSingleNode("CATEGORY").InnerText
                        End If

                        If Not nodeinner.SelectSingleNode("BILLED_AMOUNT") Is Nothing Then
                            Service_ticket.BILLED_AMOUNT = nodeinner.SelectSingleNode("BILLED_AMOUNT").InnerText
                        End If

                        If Not nodeinner.SelectSingleNode("SERVICE_PERSON_ID") Is Nothing Then
                            Service_ticket.EXP_SERVICE_PERSON_ID = nodeinner.SelectSingleNode("SERVICE_PERSON_ID").InnerText
                        End If

                        If Not nodeinner.SelectSingleNode("BILLABLE") Is Nothing Then
                            Service_ticket.EXP_BILLABLE = nodeinner.SelectSingleNode("BILLABLE").InnerText
                        End If

                        If Not nodeinner.SelectSingleNode("BILLED") Is Nothing Then
                            Service_ticket.EXP_BILLED = nodeinner.SelectSingleNode("BILLED").InnerText
                        End If

                        If Not nodeinner.SelectSingleNode("INTERNAL_LINE_NO") Is Nothing Then
                            Service_ticket.EXP_INTERNAL_LINE_NO = nodeinner.SelectSingleNode("INTERNAL_LINE_NO").InnerText
                        End If
                        Dim strSQL2 As [String]


                        If (Service_ticket.EXP_INTERNAL_LINE_NO <> "") Then
                            strSQL2 = "INSERT INTO TAB_SERVICE_TICKET_EXPENSE (SERVICE_TICKET_NO,RECORD_NO,ENTRY_DATE,CATEGORY,BILLED_AMOUNT,SERVICE_PERSON_ID,BILLABLE,BILLED,INTERNAL_LINE_NO) VALUES ('" + Service_ticket.SERVICE_TICKET_NO + "',(SELECT NVL(MAX(RECORD_NO),0)+1 FROM TAB_SERVICE_TICKET_EXPENSE WHERE SERVICE_TICKET_NO='" + Service_ticket.SERVICE_TICKET_NO + "') ,TO_DATE('" + Service_ticket.EXP_ENTRY_DATE + "','MM/DD/RRRR'),'" + Service_ticket.CATEGORY + "','" + Service_ticket.BILLED_AMOUNT + "','" + Service_ticket.EXP_SERVICE_PERSON_ID + "','" + Service_ticket.EXP_BILLABLE + "','" + Service_ticket.EXP_BILLED + "','" + Service_ticket.EXP_INTERNAL_LINE_NO + "')"
                        Else
                            strSQL2 = "UPDATE TAB_SERVICE_TICKET_EXPENSE SET TO_DATE('" + Service_ticket.EXP_ENTRY_DATE + "','MM/DD/RRRR'),'" + Service_ticket.CATEGORY + "','" + Service_ticket.BILLED_AMOUNT + "','" + Service_ticket.EXP_SERVICE_PERSON_ID + "','" + Service_ticket.EXP_BILLABLE + "','" + Service_ticket.EXP_BILLED + "','" + Service_ticket.EXP_INTERNAL_LINE_NO + "'  WHERE SERVICE_TICKET_NO='" + Service_ticket.SERVICE_TICKET_NO + "' AND RECORD_NO='" & Service_ticket.EXP_RECORD_NO & "'"
                        End If


                        con.Open()

                        cmd = New OleDbCommand(strSQL2, con)
                        Try
                            res = cmd.ExecuteNonQuery()
                        Catch ex As Exception
                            Call insert_sys_log("Exception while inserting", strSQL2)
                        End Try

                        con.Close()

                    Next
                    'End

                Catch ex As Exception
                    Call insert_sys_log("Exception while inserting", ex.Message)
                    result = "Exception while inserting" + ex.Message.ToString()
                    resp.Content = New StringContent(result, System.Text.Encoding.UTF8, "text/plain")
                    Return resp
                End Try


            Next
            con.Close()
            Dim myCommand As OleDbCommand = Nothing
            Try

                myCommand = New OleDbCommand("SP_UPDATE_TAB_TICKET_TIME")
                myCommand.Connection = con
                myCommand.CommandType = CommandType.StoredProcedure

                If (myCommand.Connection.State = Data.ConnectionState.Closed) Then
                    myCommand.Connection.Open()
                End If
                myCommand.ExecuteNonQuery()
                myCommand.Connection.Close()

            Catch ex As Exception
                Call insert_sys_log("Calling Back end procedure", ex.Message)
            End Try
            result = "successfully inserted"
            resp.Content = New StringContent(result, System.Text.Encoding.UTF8, "text/plain")
            Return resp


        End Function

        Public Sub insert_sys_log(ByVal str1 As String, ByVal message As String)
            Dim Connection As [String] = ConfigurationManager.AppSettings("dbconn.ConnectionString")
            Dim con As New OleDbConnection(Connection)
            dbad.InsertCommand = New OleDbCommand
            Dim sterr1, sterr2, sterr3, sterr4, sterr As String
            sterr = Replace(message, "'", "''")
            If (Len(sterr) > 4000) Then
                sterr1 = Mid(sterr, 1, 4000)
                If (Len(sterr) > 8000) Then
                    sterr2 = Mid(sterr, 4000, 8000)
                    If (Len(sterr) > 12000) Then
                        sterr3 = Mid(sterr, 8000, 12000)
                        If (Len(sterr) > 16000) Then
                            sterr4 = Mid(sterr, 12000, 16000)
                        Else
                            sterr4 = Mid(sterr, 12000, Len(sterr))
                        End If
                    Else
                        sterr3 = Mid(sterr, 8000, Len(sterr))
                        sterr4 = ""
                    End If
                Else
                    sterr2 = Mid(sterr, 4000, Len(sterr))
                    sterr3 = ""
                    sterr3 = ""
                    sterr4 = ""
                End If
            Else
                sterr1 = sterr
                sterr2 = ""
                sterr3 = ""
                sterr4 = ""
            End If
            Try
                dbad.InsertCommand.Connection = con
                If (dbad.InsertCommand.Connection.State = Data.ConnectionState.Closed) Then
                    dbad.InsertCommand.Connection.Open()
                End If
                dbad.InsertCommand.CommandText = "Insert into SYS_ACTIVATE_STATUS_LOG (LINE_NO, CHANGE_REQUEST_NO,  OBJECT_TYPE, OBJECT_NAME, ERROR_TEXT, STATUS,LOG_DATE,ERROR_TEXT1, ERROR_TEXT2, ERROR_TEXT3) values ((select nvl(max(to_number(line_no)),0)+1 from SYS_ACTIVATE_STATUS_LOG),'','ABLE_OFFLINE_API','" & str1 & "','" & sterr1 & "','N',sysdate,'" & sterr2 & "','" & sterr3 & "','" & sterr4 & "')"
                dbad.InsertCommand.ExecuteNonQuery()
                dbad.InsertCommand.Connection.Close()
            Catch ex As Exception

            End Try
        End Sub
    End Class
End Namespace