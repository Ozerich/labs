using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Shell;
using BLL;
using ContactsLib;
using Microsoft.Win32;

namespace ContactsApp
{
    public partial class App : Application
    {
        private ContactList ContactList;
        private Contact CurrentContact;
        private MainWindow mainWindow;
        private string FileName = null;

        public void NewContactList()
        {
            FileName = null;
            ContactList = Logic.CreateContactList();
            FillContactListbox();
            SelectContact(-1);
        }

        public bool LoadContactList()
        {
             ContactList = Logic.LoadContactList();
            FillContactListbox();
            SelectContact(-1);
            return false;
        }


        private void OnStartup(object sender, StartupEventArgs e)
        {
            mainWindow = new MainWindow(this);
            mainWindow.Show();
            LoadContactList();
            NewContactList();
        }

        public void SelectContact(int id)
        {
            if (id == -1)
                CurrentContact = null;
            else
                CurrentContact = ContactList[id];

            FillDetails();
            mainWindow.AddDetailButton.Visibility = (id == -1) ? Visibility.Collapsed : Visibility.Visible;
        }

        public void FillDetails()
        {
            mainWindow.ContactDetails.Children.Clear();

            if (CurrentContact == null)
                return;

            EditableField efn = new EditableField();
            efn.Changed += ContactName_Changed;
            efn.Label.Content = CurrentContact.Name;
            efn.Label.FontSize = 32;
            efn.Tag = CurrentContact;
            efn.Deletable = false;
            mainWindow.ContactDetails.Children.Add(efn);

            Label ll = new Label();
            ll.Content = "Group";
            ll.FontWeight = FontWeights.Bold;
            mainWindow.ContactDetails.Children.Add(ll);
            EditableField eff = new EditableField();
            eff.Changed += ContactGroup_Changed;
            eff.Label.Content = ContactList.FindGroupById(CurrentContact.Group_ID);
            eff.Tag = CurrentContact;
            eff.Deletable = false;
            mainWindow.ContactDetails.Children.Add(eff);

            if (CurrentContact.Details == null)
                CurrentContact.Details = new List<ContactDetail>();

            foreach (ContactDetail scd in CurrentContact.Details)
            {
                Label l = new Label();
                l.Content = scd.Name; ;
                l.FontWeight = FontWeights.Bold;
                mainWindow.ContactDetails.Children.Add(l);
                EditableField ef = new EditableField();
                ef.Label.Content = scd.Value;
                ef.Changed += ef_Changed;
                ef.Deleted += ef_Deleted;
                ef.Tag = scd;
                mainWindow.ContactDetails.Children.Add(ef);
            }

        }

        void ef_Deleted(EditableField obj)
        {
            CurrentContact.Details.Remove(obj.Tag as ContactDetail);
            ContactList.RemoveDetail((obj.Tag as ContactDetail));
            FillDetails();
        }

        void ContactGroup_Changed(EditableField sender, string value) 
        {
            Contact c = sender.Tag as Contact;
            c.Group_ID = ContactList.GetGroupByName(value).ID;
            ContactList.UpdateContact(c);
            
            FillContactListbox();
            FillDetails();
        }

        void ContactName_Changed(EditableField sender, string value)
        {
            Contact c = sender.Tag as Contact;
            c.Name = value;
            ContactList.UpdateContact(c);
            FillContactListbox();
            FillDetails();
        }

        public void ef_Changed(EditableField sender, string value)
        {
            (sender.Tag as ContactDetail).Value = value;
            ContactList.UpdateContactDetail((sender.Tag as ContactDetail));
            FillContactListbox();
            FillDetails();
        }

        private void FillContactListbox()
        {
            mainWindow.listBox.Children.Clear();
            foreach (ContactGroup g in ContactList.Groups)
            {
                Expander exp = new Expander();
                exp.Header = (g.Name != null) ? g.Name : "Other";
                exp.IsExpanded = true;
                mainWindow.listBox.Children.Add(exp);
                
                ListBox lbx = new ListBox();
                lbx.SelectionChanged += mainWindow.listBox_SelectionChanged;
                lbx.BorderThickness = new Thickness(0);
                exp.Content = lbx;

                foreach (Contact c in ContactList.Sorted)
                    if (c.Group_ID == g.ID)
                    {
                        ContactListItem i = new ContactListItem();
                        i.PersonName.Content = c.Name;
                        i.PersonName.FontWeight = FontWeights.Bold;
                        try
                        {
                            i.Description.Content = c.Details[0].Value;
                        }
                        catch { }
                        i.Tag = ContactList.Contacts.IndexOf(c);
                        lbx.Items.Add(i);
                    }
            }
        }

        public void CreateContact(string name)
        {
            Contact c = new Contact();
            c.Name = name;
            ContactList.Add(c);
            FillContactListbox();
        }

        public void AddSimpleDetail(string title, string value)
        {
            ContactDetail cd = new ContactDetail();
            cd.Contact_ID = CurrentContact.ID;
            cd.Name = title;
            cd.Value = value;

            ContactList.UpdateContactDetail(cd);
 

            CurrentContact.Details.Add(cd);
            FillDetails();
        }

        public void RemoveContact()
        {
            ContactList.Remove(CurrentContact);
            SelectContact(-1);
            FillContactListbox();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Извлечение текущего списка часто используемых элементов
            JumpList jumpList = new JumpList();
            JumpList.SetJumpList(Application.Current, jumpList);

            // Добавление нового объекта JumpPath для файла в папке приложения
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            path = Path.Combine(path, "lol.xml");
            if (File.Exists(path))
            {
                JumpTask jumpTask = new JumpTask();
                jumpTask.CustomCategory = "Documentation";
                jumpTask.Title = "Read the lol.xml";
                jumpTask.ApplicationPath = @"C:\Program Files\Notepad++\notepad++.exe";
                jumpTask.IconResourcePath = @"C:\Program Files\Notepad++\notepad++.exe";
                jumpTask.Arguments = path;
                jumpList.JumpItems.Add(jumpTask);
            }

            // Обновление списка часто используемых элементов
            jumpList.Apply();
        }
    }
}
