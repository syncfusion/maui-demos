using System;
using System.Collections.Generic;
using System.Text;

namespace SampleBrowser.Maui.Services
{
    public interface IPrintService
    {
        Task PrintAsync(string filePath);
    }
}