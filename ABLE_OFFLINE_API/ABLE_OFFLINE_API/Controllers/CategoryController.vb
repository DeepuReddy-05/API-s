﻿Imports System.Data.OleDb
Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Namespace Controllers
    Public Class CategoryController
        Inherits ApiController

        Protected dbad As New OleDbDataAdapter
        Dim Connection As [String] = ConfigurationManager.AppSettings("dbconn.ConnectionString")
        Dim con As New OleDbConnection(Connection)
        Protected Shared AppPath As String = System.AppDomain.CurrentDomain.BaseDirectory
        Dim sw As StreamWriter = Nothing

        Public Function GetCategory() As HttpResponseMessage
            Dim result As String = String.Empty
            Dim resp = New HttpResponseMessage(HttpStatusCode.OK)
            Try
                dbad.SelectCommand = New OleDbCommand
                dbad.SelectCommand.Connection = con

                dbad.SelectCommand.CommandText = "select * from V_EXPENSE_CATEGORY"
                Dim dsn As New Data.DataSet
                dsn.Clear()
                dbad.Fill(dsn)
                dbad.SelectCommand.Connection.Close()
                If (Not dsn.Tables(0) Is Nothing) And (dsn.Tables(0).Rows.Count > 0) Then
                    If File.Exists(AppPath + "\" + "Category.xml") Then
                        File.Delete(AppPath + "\" + "Category.xml")
                    End If
                    Dim filePath1 = AppPath + "\" + "Category.xml"

                    sw = New StreamWriter(filePath1, True)

                    sw.WriteLine("<?xml version=""1.0"" encoding=""UTF-8""?>")
                    sw.WriteLine("<ArrayOfCategory xmlns=""http://schemas.datacontract.org/2004/07/API_ABLE.Models""  xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"">")

                    For Each dr As DataRow In dsn.Tables(0).Rows

                        sw.WriteLine("<EXPENSE_CATEGORY>")
                        sw.WriteLine("<OPTION_VALUE>" & dr("OPTION_VALUE") & "</OPTION_VALUE>")
                        sw.WriteLine("<OPTION_TEXT>" & dr("OPTION_TEXT") & "</OPTION_TEXT>")
                        sw.WriteLine("</EXPENSE_CATEGORY>")

                    Next

                    sw.WriteLine("</ArrayOfCategory>")

                    sw.Flush()
                    sw.Close()

                    result = File.ReadAllText(AppPath + "\" + "Category.xml")
                    resp.Content = New StringContent(result, System.Text.Encoding.UTF8, "text/plain")
                    Return resp
                Else

                    result = "Category does not exists"
                    resp.Content = New StringContent(result, System.Text.Encoding.UTF8, "text/plain")
                    Return resp
                    Exit Function

                End If

            Catch ex As Exception
                Call insert_sys_log("Exception while creating Category XML", ex.Message)
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