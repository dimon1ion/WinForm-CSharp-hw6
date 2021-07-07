using FirstProject.Model;
using FirstProject.Repository;
using FirstProject.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirstProject.Presenter
{
    public class CustomerPresenter
    {
        ICustomerRepository repository;
        ICustomerView view;

        public CustomerPresenter(ICustomerView customerform, ICustomerRepository repository)
        {
            this.view = customerform;
            customerform.customerPresenter = this;
            this.repository = repository;
            UpdatecustomerListView();
        }
        public void UpdateCustomerView(int id)
        {
            Customer customer = repository.GetCustomerById(id);
            view.FullName = customer.Fullname;
            view.Address = customer.Address;
            view.Email = customer.Email;
            view.Citizenship = customer.Citizenship;
        }

        public void SaveCustomerView()
        {
            if (CheckText())
            {
                Customer customer = new Customer(view.FullName, view.Address, view.Email, view.Citizenship);
                repository.SaveCustomer(view.SelectedCustomer, customer);
                UpdatecustomerListView();
            }
        }

        public void NewCustomer()
        {
            if (CheckText())
            {
                Customer customer = new Customer(view.FullName, view.Address, view.Email, view.Citizenship);
                repository.AddCustomer(customer);
                UpdatecustomerListView();
                view.SelectedCustomer = repository.GetAllCustomers().ToList().Count - 1;
            }
        }

        private bool CheckText()
        {
            if (view.FullName != String.Empty && view.Address != String.Empty && view.Email != String.Empty && view.Citizenship != String.Empty)
            {
                return true;
            }
            MessageBox.Show("One of the text boxes is empty", "Error");
            return false;
        }

        private void UpdatecustomerListView()
        {
            var customerNames = repository.GetAllCustomers().Select(x => x.Fullname);
            int selectedCustomer = view.SelectedCustomer >= 0 ? view.SelectedCustomer : 0;
            view.CustomerList = customerNames.ToList();
            view.SelectedCustomer = selectedCustomer;
        }
    }
}
