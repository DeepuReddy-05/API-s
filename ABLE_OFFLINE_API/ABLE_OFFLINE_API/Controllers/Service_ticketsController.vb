Imports System.Data.OleDb
Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Namespace Controllers
    Public Class Service_ticketsController
        Inherits ApiController

        Protected dbad As New OleDbDataAdapter
        Dim Connection As [String] = ConfigurationManager.AppSettings("dbconn.ConnectionString")
        Dim con As New OleDbConnection(Connection)
        Protected Shared AppPath As String = System.AppDomain.CurrentDomain.BaseDirectory
        Dim sw As StreamWriter = Nothing
        Public Function GetService_tickets() As HttpResponseMessage
            Dim result As String = String.Empty
            Dim resp = New HttpResponseMessage(HttpStatusCode.OK)
            Try
                dbad.SelectCommand = New OleDbCommand
                dbad.SelectCommand.Connection = con

                dbad.SelectCommand.CommandText = "SELECT * FROM V_SYNC_SRV_SERVICE_TICKET"
                Dim dsn As New Data.DataSet
                dsn.Clear()
                dbad.Fill(dsn)
                dbad.SelectCommand.Connection.Close()
                If (Not dsn.Tables(0) Is Nothing) And (dsn.Tables(0).Rows.Count > 0) Then
                    If File.Exists(AppPath + "\" + "Service_tickets.xml") Then
                        File.Delete(AppPath + "\" + "Service_tickets.xml")
                    End If
                    Dim filePath1 = AppPath + "\" + "Service_tickets.xml"

                    sw = New StreamWriter(filePath1, True)

                    sw.WriteLine("<?xml version=""1.0"" encoding=""UTF-8""?>")
                    sw.WriteLine("<ArrayOfTickets xmlns=""http://schemas.datacontract.org/2004/07/API_ABLE.Models""  xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"">")

                    For Each dr As DataRow In dsn.Tables(0).Rows

                        sw.WriteLine("<SERVICE_TICKET>")

                        sw.WriteLine("<SERVICE_TICKET_NO>" & dr("SERVICE_TICKET_NO") & "</SERVICE_TICKET_NO>")
                        sw.WriteLine("<SERVICE_PERSON_ASSIGNED>" & dr("SERVICE_PERSON_ASSIGNED") & "</SERVICE_PERSON_ASSIGNED>")
                        sw.WriteLine("<CUSTOMER_NO>" & dr("CUSTOMER_NO") & "</CUSTOMER_NO>")
                        sw.WriteLine("<ARRIVE>" & dr("ARRIVE") & "</ARRIVE>")
                        sw.WriteLine("<MACHINE_MODEL>" & dr("MACHINE_MODEL") & "</MACHINE_MODEL>")
                        sw.WriteLine("<CUSTOMER_NAME>" & dr("CUSTOMER_NAME").ToString().Replace("&", "&amp;") & "</CUSTOMER_NAME>")
                        sw.WriteLine("<DEPART>" & dr("DEPART") & "</DEPART>")
                        sw.WriteLine("<MACHINE_SERIAL_NO>" & dr("MACHINE_SERIAL_NO") & "</MACHINE_SERIAL_NO>")
                        sw.WriteLine("<ADDRESS_1>" & dr("ADDRESS_1") & "</ADDRESS_1>")
                        sw.WriteLine("<CUT_HRS>" & dr("CUT_HRS") & "</CUT_HRS>")
                        sw.WriteLine("<CONTROL_SERIAL_NO>" & dr("CONTROL_SERIAL_NO") & "</CONTROL_SERIAL_NO>")
                        sw.WriteLine("<ADDRESS_2>" & dr("ADDRESS_2") & "</ADDRESS_2>")
                        sw.WriteLine("<OPERATING_HRS>" & dr("OPERATING_HRS") & "</OPERATING_HRS>")
                        sw.WriteLine("<CITY>" & dr("CITY") & "</CITY>")
                        sw.WriteLine("<POWER_ON_HRS>" & dr("POWER_ON_HRS") & "</POWER_ON_HRS>")
                        sw.WriteLine("<STATE>" & dr("STATE") & "</STATE>")
                        sw.WriteLine("<SAFETY_HAZARD_OBSERVED>" & dr("SAFETY_HAZARD_OBSERVED") & "</SAFETY_HAZARD_OBSERVED>")
                        sw.WriteLine("<CONTROL_MODEL>" & dr("CONTROL_MODEL") & "</CONTROL_MODEL>")
                        sw.WriteLine("<ZIP>" & dr("ZIP") & "</ZIP>")
                        sw.WriteLine("<TYPE>" & dr("TYPE") & "</TYPE>")
                        sw.WriteLine("<SALES_PERSON_SHOW_UP>" & dr("SALES_PERSON_SHOW_UP") & "</SALES_PERSON_SHOW_UP>")
                        sw.WriteLine("<CONTACT_PHONE>" & dr("CONTACT_PHONE") & "</CONTACT_PHONE>")
                        sw.WriteLine("<CONTACT_EMAIL>" & dr("CONTACT_EMAIL") & "</CONTACT_EMAIL>")
                        sw.WriteLine("<SERVICE_PERFORMED>" & dr("SERVICE_PERFORMED") & "</SERVICE_PERFORMED>")
                        sw.WriteLine("<PROBLEM_DESCRIPTION>" & dr("PROBLEM_DESCRIPTION").ToString().Replace("&", "&amp;") & "</PROBLEM_DESCRIPTION>")
                        sw.WriteLine("<FOLLOWUP_REQUIRED>" & dr("FOLLOWUP_REQUIRED") & "</FOLLOWUP_REQUIRED>")
                        'added TRAVEL_HOURS on Header Region 6 FEB 2020
                        sw.WriteLine("<TRAVEL_HOURS>" & dr("TRAVEL_HOURS") & "</TRAVEL_HOURS>")
                        'ended TRAVEL_HOURS on Header Region 6 FEB 2020


                        dbad.SelectCommand = New OleDbCommand
                        dbad.SelectCommand.Connection = con

                        dbad.SelectCommand.CommandText = "SELECT * FROM V_SYNC_SRV_SERVICE_TICKET_TIME WHERE SERVICE_TICKET_NO='" & dr("SERVICE_TICKET_NO") & "'"
                        Dim dst As New Data.DataSet
                        dst.Clear()
                        dbad.Fill(dst)
                        dbad.SelectCommand.Connection.Close()
                        If (Not dst.Tables(0) Is Nothing) And (dst.Tables(0).Rows.Count > 0) Then

                            For Each drt As DataRow In dst.Tables(0).Rows
                                sw.WriteLine("<TICKET_TIME>")

                                sw.WriteLine("<RECORD_NO>" & drt("RECORD_NO") & "</RECORD_NO>")
                                sw.WriteLine("<ENTRY_DATE>" & drt("ENTRY_DATE") & "</ENTRY_DATE>")
                                sw.WriteLine("<TIME_IN_HRS>" & drt("TIME_IN_HRS") & "</TIME_IN_HRS>")
                                sw.WriteLine("<TIME_IN_MINS>" & drt("TIME_IN_MINS") & "</TIME_IN_MINS>")
                                sw.WriteLine("<TIME_OUT_HRS>" & drt("TIME_OUT_HRS") & "</TIME_OUT_HRS>")
                                sw.WriteLine("<TIME_OUT_MINS>" & drt("TIME_OUT_MINS") & "</TIME_OUT_MINS>")
                                sw.WriteLine("<WORK_HOURS>" & drt("WORK_HOURS") & "</WORK_HOURS>")
                                sw.WriteLine("<TRAVEL_HOURS>" & drt("TRAVEL_HOURS") & "</TRAVEL_HOURS>")
                                sw.WriteLine("<LUNCH_HOURS>" & drt("LUNCH_HOURS") & "</LUNCH_HOURS>")
                                sw.WriteLine("<SERVICE_PERSON_ID>" & drt("SERVICE_PERSON_ID") & "</SERVICE_PERSON_ID>")
                                sw.WriteLine("<BILLABLE>" & drt("BILLABLE") & "</BILLABLE>")
                                sw.WriteLine("<BILLED>" & drt("BILLED") & "</BILLED>")
                                sw.WriteLine("<SIGNATURE_PATH>" & drt("SIGNATURE_PATH") & "</SIGNATURE_PATH>")
                                sw.WriteLine("<INTERNAL_LINE_NO></INTERNAL_LINE_NO>")
                                sw.WriteLine("</TICKET_TIME>")
                            Next
                        End If


                        dbad.SelectCommand = New OleDbCommand
                        dbad.SelectCommand.Connection = con

                        dbad.SelectCommand.CommandText = "SELECT * FROM V_SYNC_SRV_SERVICE_TICKET_EXP WHERE SERVICE_TICKET_NO='" & dr("SERVICE_TICKET_NO") & "'"
                        Dim dse As New Data.DataSet
                        dse.Clear()
                        dbad.Fill(dse)
                        dbad.SelectCommand.Connection.Close()
                        If (Not dse.Tables(0) Is Nothing) And (dse.Tables(0).Rows.Count > 0) Then

                            For Each dre As DataRow In dse.Tables(0).Rows
                                sw.WriteLine("<TICKET_EXPENSE>")

                                sw.WriteLine("<RECORD_NO>" & dre("RECORD_NO") & "</RECORD_NO>")
                                sw.WriteLine("<ENTRY_DATE>" & dre("ENTRY_DATE") & "</ENTRY_DATE>")
                                sw.WriteLine("<CATEGORY>" & dre("CATEGORY") & "</CATEGORY>")
                                sw.WriteLine("<BILLED_AMOUNT>" & dre("BILLED_AMOUNT") & "</BILLED_AMOUNT>")
                                sw.WriteLine("<SERVICE_PERSON_ID>" & dre("SERVICE_PERSON_ID") & "</SERVICE_PERSON_ID>")
                                sw.WriteLine("<BILLABLE>" & dre("BILLABLE") & "</BILLABLE>")
                                sw.WriteLine("<BILLED>" & dre("BILLED") & "</BILLED>")
                                sw.WriteLine("<INTERNAL_LINE_NO></INTERNAL_LINE_NO>")
                                sw.WriteLine("</TICKET_EXPENSE>")
                            Next
                        End If


                        sw.WriteLine("</SERVICE_TICKET>")

                    Next

                    sw.WriteLine("</ArrayOfTickets>")

                    sw.Flush()
                    sw.Close()

                    result = File.ReadAllText(AppPath + "\" + "Service_tickets.xml")
                    resp.Content = New StringContent(result, System.Text.Encoding.UTF8, "text/plain")
                    Return resp
                Else

                    result = "Service tickets does not exists"
                    resp.Content = New StringContent(result, System.Text.Encoding.UTF8, "text/plain")
                    Return resp
                    Exit Function

                End If

            Catch ex As Exception
                Call insert_sys_log("Exception while creating Service tickets XML", ex.Message)
                resp.Content = New StringContent(ex.Message, System.Text.Encoding.UTF8, "text/plain")
                Return resp
                Exit Function
            End Try
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