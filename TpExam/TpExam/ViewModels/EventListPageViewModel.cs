﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using TpExam.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace TpExam.ViewModels
{
    /// <summary>
    /// ViewModel for event list page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class EventListPageViewModel : BaseViewModel
    {
        #region Fields

        private static EventListPageViewModel eventListViewModel;

        private List<EventList> eventItems;

        private List<EventList> popularEventItems;

        private List<EventList> upcomingEventItems;

        private int selectedIndex;

        private string searchText;

        private string allListSearchText;

        private string upcomingListSearchText;

        private string popularListSearchText;

        private Command menuCommand;

        private Command itemTappedCommand;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="EventListPageViewModel" /> class.
        /// </summary>
        public EventListPageViewModel()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of event list view model.
        /// </summary>
        public static EventListPageViewModel BindingContext =>
            eventListViewModel = PopulateData<EventListPageViewModel>("catalog.json");

        /// <summary>
        /// Gets or sets the event items collection.
        /// </summary>
        [DataMember(Name = "eventItems")]
        public List<EventList> EventItems
        {
            get
            {
                return this.eventItems;
            }

            set
            {
                this.SetProperty(ref this.eventItems, value);
            }
        }

        /// <summary>
        /// Gets the upcoming events collection.
        /// </summary>
        public List<EventList> UpcomingEventItems
        {
            get
            {
                return this.upcomingEventItems = this.EventItems.Where(item => item.IsUpcoming == true).ToList();
            }

            private set
            {
                this.SetProperty(ref this.upcomingEventItems, value);
            }
        }

        /// <summary>
        /// Gets the popular events collection.
        /// </summary>
        public List<EventList> PopularEventItems
        {
            get
            {
                return this.popularEventItems = this.EventItems.Where(item => item.IsPopular == true).ToList();
            }

            private set
            {
                this.SetProperty(ref this.popularEventItems, value);
            }
        }

        /// <summary>
        /// Gets or sets the selected index of the event.
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                return this.selectedIndex;
            }

            set
            {
                this.selectedIndex = value;
                this.SearchText = string.Empty;
            }
        }

        /// <summary>
        /// Gets or sets the search text in the event.
        /// </summary>
        public string SearchText
        {
            get
            {
                return this.searchText;
            }

            set
            {
                this.UpdateSelectedText();
                this.SetProperty(ref this.searchText, value);
            }
        }

        /// <summary>
        /// Gets or sets the all list search text in the collection.
        /// </summary>
        public string AllListSearchText
        {
            get
            {
                return this.allListSearchText;
            }

            set
            {
                this.SetProperty(ref this.allListSearchText, value);
            }
        }

        /// <summary>
        /// Gets or sets the upcoming list search text in the collection.
        /// </summary>
        public string UpcomingListSearchText
        {
            get
            {
                return this.upcomingListSearchText;
            }

            set
            {
                this.SetProperty(ref this.upcomingListSearchText, value);
            }
        }

        /// <summary>
        /// Gets or sets the popular list search text collection.
        /// </summary>
        public string PopularListSearchText
        {
            get
            {
                return this.popularListSearchText;
            }

            set
            {
                this.SetProperty(ref this.popularListSearchText, value);
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets the command that is executed when the item is selected.
        /// </summary>
        public Command ItemTappedCommand
        {
            get
            {
                return this.itemTappedCommand ?? (this.itemTappedCommand = new Command(this.ItemSelected));
            }
        }

        /// <summary>
        /// Gets the command that is executed when the menu button is clicked.
        /// </summary>
        public Command MenuCommand
        {
            get
            {
                return this.menuCommand ?? (this.menuCommand = new Command(this.MenuButtonClicked));
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Populates the data for view model from json file.
        /// </summary>
        /// <typeparam name="T">Type of view model.</typeparam>
        /// <param name="fileName">Json file to fetch data.</param>
        /// <returns>Returns the view model object.</returns>
        private static T PopulateData<T>(string fileName)
        {
            var file = "TpExam.Data." + fileName;

            var assembly = typeof(App).GetTypeInfo().Assembly;

            T data;

            using (var stream = assembly.GetManifestResourceStream(file))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                data = (T)serializer.ReadObject(stream);
            }

            return data;
        }

        /// <summary>
        /// Invoked when item is clicked.
        /// </summary>
        private void ItemSelected(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when menu button is changed.
        /// </summary>
        private void MenuButtonClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when search text is changed.
        /// </summary>
        private void UpdateSelectedText()
        {
            switch (this.selectedIndex)
            {
                case 0:
                    this.AllListSearchText = this.searchText;
                    break;

                case 1:
                    this.UpcomingListSearchText = this.searchText;
                    break;

                case 2:
                    this.PopularListSearchText = this.searchText;
                    break;
            }
        }

        #endregion
    }
}
