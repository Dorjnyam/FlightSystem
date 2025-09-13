namespace FlightSystem.CheckinApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            
            // Start directly with LoginForm
            var loginForm = new LoginForm();
            if (loginForm.ShowDialog() == DialogResult.OK && loginForm.LoggedInEmployee != null)
            {
                // Show main check-in system form after successful login
                var mainForm = new CheckinMainSystemForm(loginForm.LoggedInEmployee);
                Application.Run(mainForm);
            }
        }
    }
}