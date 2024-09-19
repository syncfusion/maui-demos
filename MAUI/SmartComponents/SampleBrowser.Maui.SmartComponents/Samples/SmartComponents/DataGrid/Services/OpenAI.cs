#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Azure.AI.OpenAI;
using Azure;
using Microsoft.SemanticKernel.ChatCompletion;

namespace SampleBrowser.Maui.SmartComponents.SmartComponents;
public class OpenAi : AzureBaseService
{
    internal string GetAnamolyDetectionResponse()
    {
        return "{\r\n  \"DataSource\": [\r\n    {\r\n      \"MachineID\": \"M002\",\r\n      \"Temperature\": 788,\r\n      \"Pressure\": 115,\r\n      \"Voltage\": 230,\r\n      \"MotorSpeed\": 1520,\r\n      \"ProductionRate\": 105,\r\n      \"AnomalyFieldName\": \"Temperature\",\r\n      \"AnomalyDescription\": \"The temperature is too high compared to expected values, hence marked as anomaly data.\"\r\n    },\r\n    {\r\n      \"MachineID\": \"M005\",\r\n      \"Temperature\": 92,\r\n      \"Pressure\": 116,\r\n      \"Voltage\": 222,\r\n      \"MotorSpeed\": 21457,\r\n      \"ProductionRate\": 980,\r\n      \"AnomalyFieldName\": \"MotorSpeed\",\r\n      \"AnomalyDescription\": \"The motor speed is excessively high compared to expected values, hence marked as anomaly data.\"\r\n    },\r\n    {\r\n      \"MachineID\": \"M008\",\r\n      \"Temperature\": 90,\r\n      \"Pressure\": 1120,\r\n      \"Voltage\": 225,\r\n      \"MotorSpeed\": 1470,\r\n      \"ProductionRate\": 89,\r\n      \"AnomalyFieldName\": \"Pressure\",\r\n      \"AnomalyDescription\": \"The pressure is excessively high compared to expected values, hence marked as anomaly data.\"\r\n    },\r\n    {\r\n      \"MachineID\": \"M0011\",\r\n      \"Temperature\": 86,\r\n      \"Pressure\": 118,\r\n      \"Voltage\": 2221,\r\n      \"MotorSpeed\": 1925,\r\n      \"ProductionRate\": 103,\r\n      \"AnomalyFieldName\": \"Voltage\",\r\n      \"AnomalyDescription\": \"The voltage is too high compared to expected values, hence marked as anomaly data.\"\r\n    }\r\n  ]\r\n}\r\n";
    }

    internal string GetDataPredictionResponse()
    {
        return "{\r\n  \"GenerateDataSource\": [\r\n    {\r\n      \"StudentID\": 2001,\r\n      \"StudentName\": \"John Smith\",\r\n      \"FirstYearGPA\": 4.7,\r\n      \"SecondYearGPA\": 4.1,\r\n      \"ThirdYearGPA\": 5.0,\r\n      \"FinalYearGPA\": 4.41,\r\n      \"TotalGPA\": 4.55,\r\n      \"TotalGrade\": \"A+\"\r\n    },\r\n    {\r\n      \"StudentID\": 2002,\r\n      \"StudentName\": \"Emily Davis\",\r\n      \"FirstYearGPA\": 3.3,\r\n      \"SecondYearGPA\": 3.5,\r\n      \"ThirdYearGPA\": 3.7,\r\n      \"FinalYearGPA\": 3.310,\r\n      \"TotalGPA\": 3.45,\r\n      \"TotalGrade\": \"B\"\r\n    },\r\n    {\r\n      \"StudentID\": 2003,\r\n      \"StudentName\": \"Micheal Lee\",\r\n      \"FirstYearGPA\": 3.9,\r\n      \"SecondYearGPA\": 3.8,\r\n      \"ThirdYearGPA\": 3.9,\r\n      \"FinalYearGPA\": 3.67,\r\n      \"TotalGPA\": 3.82,\r\n      \"TotalGrade\": \"B+\"\r\n    },\r\n    {\r\n      \"StudentID\": 2004,\r\n      \"StudentName\": \"Sarah Brown\",\r\n      \"FirstYearGPA\": 2.0,\r\n      \"SecondYearGPA\": 2.7,\r\n      \"ThirdYearGPA\": 2.5,\r\n      \"FinalYearGPA\": 2.20,\r\n      \"TotalGPA\": 2.35,\r\n      \"TotalGrade\": \"C\"\r\n    },\r\n    {\r\n      \"StudentID\": 2005,\r\n      \"StudentName\": \"James Wilson\",\r\n      \"FirstYearGPA\": 3.0,\r\n      \"SecondYearGPA\": 3.5,\r\n      \"ThirdYearGPA\": 3.2,\r\n      \"FinalYearGPA\": 3.03,\r\n      \"TotalGPA\": 3.18,\r\n      \"TotalGrade\": \"B\"\r\n    },\r\n    {\r\n      \"StudentID\": 2006,\r\n      \"StudentName\": \"Sarah Jane\",\r\n      \"FirstYearGPA\": 3.7,\r\n      \"SecondYearGPA\": 3.0,\r\n      \"ThirdYearGPA\": 4.3,\r\n      \"FinalYearGPA\": 3.47,\r\n      \"TotalGPA\": 3.62,\r\n      \"TotalGrade\": \"B+\"\r\n    },\r\n    {\r\n      \"StudentID\": 2007,\r\n      \"StudentName\": \"Emily Rose\",\r\n      \"FirstYearGPA\": 5.0,\r\n      \"SecondYearGPA\": 4.9,\r\n      \"ThirdYearGPA\": 4.8,\r\n      \"FinalYearGPA\": 4.70,\r\n      \"TotalGPA\": 4.85,\r\n      \"TotalGrade\": \"A+\"\r\n    },\r\n    {\r\n      \"StudentID\": 2008,\r\n      \"StudentName\": \"John Michael\",\r\n      \"FirstYearGPA\": 4.0,\r\n      \"SecondYearGPA\": 4.1,\r\n      \"ThirdYearGPA\": 4.2,\r\n      \"FinalYearGPA\": 3.90,\r\n      \"TotalGPA\": 4.05,\r\n      \"TotalGrade\": \"A\"\r\n    },\r\n    {\r\n      \"StudentID\": 2009,\r\n      \"StudentName\": \"David James\",\r\n      \"FirstYearGPA\": 1.5,\r\n      \"SecondYearGPA\": 2.2,\r\n      \"ThirdYearGPA\": 2.3,\r\n      \"FinalYearGPA\": 1.85,\r\n      \"TotalGPA\": 1.95,\r\n      \"TotalGrade\": \"F\"\r\n    },\r\n    {\r\n      \"StudentID\": 2010,\r\n      \"StudentName\": \"Mary Ann\",\r\n      \"FirstYearGPA\": 2.7,\r\n      \"SecondYearGPA\": 2.1,\r\n      \"ThirdYearGPA\": 3.0,\r\n      \"FinalYearGPA\": 2.40,\r\n      \"TotalGPA\": 2.55,\r\n      \"TotalGrade\": \"C\"\r\n    },\r\n    {\r\n      \"StudentID\": 2011,\r\n      \"StudentName\": \"Robert Paul\",\r\n      \"FirstYearGPA\": 2.9,\r\n      \"SecondYearGPA\": 3.7,\r\n      \"ThirdYearGPA\": 3.9,\r\n      \"FinalYearGPA\": 3.30,\r\n      \"TotalGPA\": 3.45,\r\n      \"TotalGrade\": \"B\"\r\n    },\r\n    {\r\n      \"StudentID\": 2012,\r\n      \"StudentName\": \"Laura Grace\",\r\n      \"FirstYearGPA\": 4.0,\r\n      \"SecondYearGPA\": 3.1,\r\n      \"ThirdYearGPA\": 3.7,\r\n      \"FinalYearGPA\": 3.40,\r\n      \"TotalGPA\": 3.55,\r\n      \"TotalGrade\": \"B+\"\r\n    },\r\n    {\r\n      \"StudentID\": 2013,\r\n      \"StudentName\": \"William Henry\",\r\n      \"FirstYearGPA\": 4.0,\r\n      \"SecondYearGPA\": 4.1,\r\n      \"ThirdYearGPA\": 4.2,\r\n      \"FinalYearGPA\": 3.90,\r\n      \"TotalGPA\": 4.05,\r\n      \"TotalGrade\": \"A\"\r\n    },\r\n    {\r\n      \"StudentID\": 2014,\r\n      \"StudentName\": \"Anna Marie\",\r\n      \"FirstYearGPA\": 4.0,\r\n      \"SecondYearGPA\": 4.0,\r\n      \"ThirdYearGPA\": 4.1,\r\n      \"FinalYearGPA\": 3.83,\r\n      \"TotalGPA\": 3.98,\r\n      \"TotalGrade\": \"B+\"\r\n    },\r\n    {\r\n      \"StudentID\": 2015,\r\n      \"StudentName\": \"Charles Thomas\",\r\n      \"FirstYearGPA\": 4.7,\r\n      \"SecondYearGPA\": 4.8,\r\n      \"ThirdYearGPA\": 4.9,\r\n      \"FinalYearGPA\": 4.60,\r\n      \"TotalGPA\": 4.75,\r\n      \"TotalGrade\": \"A+\"\r\n    },\r\n    {\r\n      \"StudentID\": 2016,\r\n      \"StudentName\": \"Jennifer Lynn\",\r\n      \"FirstYearGPA\": 3.0,\r\n      \"SecondYearGPA\": 3.1,\r\n      \"ThirdYearGPA\": 3.2,\r\n      \"FinalYearGPA\": 2.90,\r\n      \"TotalGPA\": 3.05,\r\n      \"TotalGrade\": \"B\"\r\n    },\r\n    {\r\n      \"StudentID\": 2017,\r\n      \"StudentName\": \"Christopher Lee\",\r\n      \"FirstYearGPA\": 3.9,\r\n      \"SecondYearGPA\": 3.9,\r\n      \"ThirdYearGPA\": 4.2,\r\n      \"FinalYearGPA\": 3.80,\r\n      \"TotalGPA\": 3.95,\r\n      \"TotalGrade\": \"B+\"\r\n    },\r\n    {\r\n      \"StudentID\": 2018,\r\n      \"StudentName\": \"Elizabeth Claire\",\r\n      \"FirstYearGPA\": 2.0,\r\n      \"SecondYearGPA\": 2.1,\r\n      \"ThirdYearGPA\": 2.2,\r\n      \"FinalYearGPA\": 1.90,\r\n      \"TotalGPA\": 2.05,\r\n      \"TotalGrade\": \"F\"\r\n    },\r\n    {\r\n      \"StudentID\": 2019,\r\n      \"StudentName\": \"Daniel Scott\",\r\n      \"FirstYearGPA\": 4.0,\r\n      \"SecondYearGPA\": 4.1,\r\n      \"ThirdYearGPA\": 3.3,\r\n      \"FinalYearGPA\": 3.60,\r\n      \"TotalGPA\": 3.75,\r\n      \"TotalGrade\": \"B+\"\r\n    },\r\n    {\r\n      \"StudentID\": 2020,\r\n      \"StudentName\": \"Megan Louise\",\r\n      \"FirstYearGPA\": 3.0,\r\n      \"SecondYearGPA\": 3.5,\r\n      \"ThirdYearGPA\": 3.5,\r\n      \"FinalYearGPA\": 3.13,\r\n      \"TotalGPA\": 3.28,\r\n      \"TotalGrade\": \"B\"\r\n    }\r\n  ]\r\n}\r\n";
    }

    public async Task<string?> GetResponseFromOpenAI(string prompt)
    {
        if (IsCredentialValid)
        {
            try
            {
                if (ChatHistory != null)
                {
                    ChatHistory.Clear();
                    // Add the user's prompt as a user message to the conversation.
                    ChatHistory.AddSystemMessage("You are a predictive analytics assistant.");
                    // Add the user's prompt as a user message to the conversation.
                    ChatHistory.AddUserMessage(prompt);
                    if (ChatCompletions != null)
                    {
                        //// Send the chat completion request to the OpenAI API and await the response.
                        var response = await ChatCompletions.GetChatMessageContentAsync(chatHistory: ChatHistory, kernel: Kernel);
                        return response.ToString();
                    }
                }
                return null;

            }
            catch
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }
}
