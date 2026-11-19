namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
    using System;
    using System.Collections.ObjectModel;
    using Microsoft.Maui.Controls;
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.Scheduler;

    /// <summary>
    /// Wiring for the ICS Export/Import sample buttons.
    /// The two top-bar buttons drive the scheduler's
    /// <see cref="SfScheduler.ExportToICalendar(string)"/> and
    /// <see cref="SfScheduler.ImportICalendar"/> APIs.
    /// </summary>
    internal class ICSExportImportBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// The scheduler instance.
        /// </summary>
        private SfScheduler? scheduler;

        /// <summary>
        /// The export button.
        /// </summary>
        private Button? exportButton;

        /// <summary>
        /// The import button.
        /// </summary>
        private Button? importButton;

        /// <inheritdoc/>
        protected override void OnAttachedTo(SampleView bindable)
        {
            base.OnAttachedTo(bindable);

            this.scheduler = bindable.Content.FindByName<SfScheduler>("Scheduler");
            this.exportButton = bindable.FindByName<Button>("ExportButton");
            this.importButton = bindable.FindByName<Button>("ImportButton");

            if (this.exportButton != null)
            {
                this.exportButton.Clicked += OnExportClicked;
            }

            if (this.importButton != null)
            {
                this.importButton.Clicked += OnImportClicked;
            }
        }

        /// <summary>
        /// Exports the scheduler appointments to a local .ics file.
        /// </summary>
        private async void OnExportClicked(object? sender, EventArgs e)
        {
            if (this.scheduler == null)
            {
                return;
            }

            bool exported = await this.scheduler.ExportToICalendar("CalendarICS");
            var page = Application.Current?.Windows[0].Page;
            if (page == null)
            {
                return;
            }

            if (exported)
            {
                await page.DisplayAlertAsync("Success", "Appointments exported successfully.", "OK");
            }
        }

        /// <summary>
        /// Imports scheduled appointments from a user-picked .ics file.
        /// </summary>
        private async void OnImportClicked(object? sender, EventArgs e)
        {
            if (this.scheduler == null)
            {
                return;
            }

            bool imported = await this.scheduler.ImportICalendar();
            var page = Application.Current?.Windows[0].Page;
            if (page == null)
            {
                return;
            }

            if (imported)
            {
                await page.DisplayAlertAsync("Success", "Appointments imported successfully.", "OK");
            }
        }

        /// <inheritdoc/>
        protected override void OnDetachingFrom(SampleView bindable)
        {
            base.OnDetachingFrom(bindable);

            if (this.exportButton != null)
            {
                this.exportButton.Clicked -= OnExportClicked;
                this.exportButton = null;
            }

            if (this.importButton != null)
            {
                this.importButton.Clicked -= OnImportClicked;
                this.importButton = null;
            }

            if (this.scheduler != null)
            {
                this.scheduler = null;
            }
        }
    }
}