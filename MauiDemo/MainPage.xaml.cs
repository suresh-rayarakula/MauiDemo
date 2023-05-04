using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace MauiDemo;

public partial class MainPage : ContentPage
{
    int count = 0;
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly string _apiUrl = "http://localhost:8010/api/Employee";

    public MainPage()
    {
        InitializeComponent();
        LoadEmployees();
    }
    private async void OnViewClicked(object sender, EventArgs e)
    {
        await LoadEmployees();
    }
    private async Task LoadEmployees()
    {
        try
        {
            var response = await _httpClient.GetAsync(_apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };
                var employees = System.Text.Json.JsonSerializer.Deserialize<List<Employee>>(content, options);

                employeesListView.ItemsSource = employees;
            }
            else
            {
                // handle error response
            }
        }
        catch (Exception ex)
        {
            // handle exception
        }
    }
    private async void OnCreateClicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(FirstNameEntry.Text) || string.IsNullOrEmpty(LastNameEntry.Text))
            {
                await DisplayAlert("Validation Error", "Please enter first name and last name.", "OK");
                return;
            }
            var employee = new Employee
            {
                FirstName = FirstNameEntry.Text,
                LastName = LastNameEntry.Text,
                Email = EmailEntry.Text,
                Phone = PhoneEntry.Text,
                Address = AddressEntry.Text,
                HireDate = HireDateDatePicker.Date,
                Salary = decimal.Parse(SalaryEntry.Text)
            };

            var json = JsonConvert.SerializeObject(employee);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = await client.PostAsync(_apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                // Refresh the list of employees
                await LoadEmployees();
            }
        }
        catch (Exception ex)
        {
            // handle exception
        }
    }
    private void OnClearClicked(object sender, EventArgs e)
    {
        clear();
    }
    private void clear()
    {
        EmployeeIDLabel.Text = "";
        FirstNameEntry.Text = "";
        LastNameEntry.Text = "";
        EmailEntry.Text = "";
        PhoneEntry.Text = "";
        AddressEntry.Text = "";
        HireDateDatePicker.Date = DateTime.Now.Date;
        SalaryEntry.Text = "0";
    }
    private async void OnUpdateClicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(EmployeeIDLabel.Text) || Convert.ToInt32(EmployeeIDLabel.Text) <= 0)
            {
                await DisplayAlert("Update Error", "Please Select Record ", "Ok");
                return;
            }
            var employee = new Employee
            {
                EmployeeID = int.Parse(EmployeeIDLabel.Text),
                FirstName = FirstNameEntry.Text,
                LastName = LastNameEntry.Text,
                Email = EmailEntry.Text,
                Phone = PhoneEntry.Text,
                Address = AddressEntry.Text,
                HireDate = HireDateDatePicker.Date,
                Salary = decimal.Parse(SalaryEntry.Text)
            };

            var json = JsonConvert.SerializeObject(employee);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = await client.PutAsync(_apiUrl + $"/{employee.EmployeeID}", content);

            if (response.IsSuccessStatusCode)
            {
                // Refresh the list of employees
                await LoadEmployees();
            }
            clear();
        }
        catch (Exception ex)
        {
            // handle exception
        }
    }
    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(EmployeeIDLabel.Text) || Convert.ToInt32(EmployeeIDLabel.Text) <= 0)
            {
                await DisplayAlert("Delete Error", "Please Select Record", "Ok");
                return;
            }
            else
            {
                bool confirmed = await DisplayAlert("Delete Employee", "Are you sure you want to delete this employee?", "Yes", "No");
                if (confirmed)
                {
                    var employeeID = int.Parse(EmployeeIDLabel.Text);

                    var client = new HttpClient();
                    var response = await client.DeleteAsync(_apiUrl + $"/{employeeID}");

                    if (response.IsSuccessStatusCode)
                    {
                        // Refresh the list of employees
                        await LoadEmployees();
                    }
                }
                clear();
            }
        }
        catch (Exception ex)
        {
            // handle exception
        }
    }
    private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            if (e.SelectedItem == null)
                return;

            var employee = (Employee)e.SelectedItem;

            // Display the employee details in the UI
            EmployeeIDLabel.Text = employee.EmployeeID.ToString();
            FirstNameEntry.Text = employee.FirstName;
            LastNameEntry.Text = employee.LastName;
            EmailEntry.Text = employee.Email;
            PhoneEntry.Text = employee.Phone;
            AddressEntry.Text = employee.Address;
            HireDateDatePicker.Date = employee.HireDate;
            SalaryEntry.Text = employee.Salary.ToString();

            // Deselect the ListView item
            ((ListView)sender).SelectedItem = null;
        }
        catch (Exception ex)
        {
            // handle exception
        }
    }

}

