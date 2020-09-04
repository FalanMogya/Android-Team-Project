using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Android;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.RecyclerView.Extensions;
using Android.Support.V7.View.Menu;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.Json;
using static Android.Content.ClipData;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using Java.Security;
using AlertDialog = Android.Support.V7.App.AlertDialog;
using Android.Views.InputMethods;
using Android.Content;

namespace AndroidMasterDetail
{
    public class Root
    {
        public List<Category> categories { get; set; }
        public List<Subcategory> subcategories { get; set; }
        public List<Entry> entries { get; set; }
    }
    public class Category
    {
        public string categoryId { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public List<string> children { get; set; }
    }
    public class Subcategory
    {
        public string subcategoryId { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public List<string> children { get; set; }
    }
    public class Entry
    {
        public string entryId { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public List<object> children { get; set; }
        public string description { get; set; }
        public string examples { get; set; }
        public bool license { get; set; }
        public bool temperatureControl { get; set; }
        public bool snapEligible { get; set; }
        public bool testingRequired { get; set; }
        public string regulation { get; set; }
    }
    [Activity(Label = "KSRE", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        ListView mainList;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);


            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);

            int currentLayout = 0;
            //Reads the JSON, creates an object from it.
            StreamReader strm = new StreamReader(Assets.Open("categories.json"));
            string json = strm.ReadToEnd();
            Root root = JsonConvert.DeserializeObject<Root>(json);

            //Declarations for buttons
            Button firstButton = FindViewById<Button>(Resource.Id.firstButton);
            Button secondButton = FindViewById<Button>(Resource.Id.secondButton);
            Button thirdButton = FindViewById<Button>(Resource.Id.thirdButton);
            Button fourthButton = FindViewById<Button>(Resource.Id.fourthButton);
            Button fifthButton = FindViewById<Button>(Resource.Id.fifthButton);
            Button sixthButton = FindViewById<Button>(Resource.Id.sixthButton);
            Button seventhButton = FindViewById<Button>(Resource.Id.seventhButton);
            Button eighthButton = FindViewById<Button>(Resource.Id.eighthButton);
            Button backButton = FindViewById<Button>(Resource.Id.backButton);
            Button bakedButton = FindViewById<Button>(Resource.Id.bakedButton);
            Button preparedButton = FindViewById<Button>(Resource.Id.preparedButton);
            Button alcButton = FindViewById<Button>(Resource.Id.alcButton);
            Button nonAlcButton = FindViewById<Button>(Resource.Id.nonAlcButton);
            Button jamButton = FindViewById<Button>(Resource.Id.jamButton);
            Button jellyButton = FindViewById<Button>(Resource.Id.jellyButton);
            Button shelfStableButton = FindViewById<Button>(Resource.Id.shelfStableButton);
            Button meatButton = FindViewById<Button>(Resource.Id.meatButton);
            Button eggsButton = FindViewById<Button>(Resource.Id.eggsButton);
            Button dairyButton = FindViewById<Button>(Resource.Id.dairyButton);



            //Declarations for sets of buttons
            LinearLayout firstLayout = FindViewById<LinearLayout>(Resource.Id.leftCol);
            LinearLayout secondLayout = FindViewById<LinearLayout>(Resource.Id.rightCol);
            LinearLayout firstClick = FindViewById<LinearLayout>(Resource.Id.firstClick);
            LinearLayout secondClick = FindViewById<LinearLayout>(Resource.Id.secondClick);
            LinearLayout thirdClick = FindViewById<LinearLayout>(Resource.Id.thirdClick);
            LinearLayout fourthClick = FindViewById<LinearLayout>(Resource.Id.fourthClick);

            //Declaration for list's layout
            LinearLayout listLayout = FindViewById<LinearLayout>(Resource.Id.listLayout);
            LinearLayout listLayout2 = FindViewById<LinearLayout>(Resource.Id.listLayout2);

            Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
            //Declaration for ListView and it's items
            BindingList<string> items = new BindingList<string>();
            mainList = (ListView)FindViewById<ListView>(Resource.Id.mainlistview);
            mainList.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, items);
            mainList.ItemClick += (s, e) => {
                var t = items[e.Position];
                string itemSelected = items[e.Position];
                foreach (Entry i in root.entries)
                {
                    if(itemSelected == i.name)
                    {
                        alert.SetMessage(i.regulation);
                        alert.Show();
                    }
                }
            };

            //SearchView Declaration
            BindingList<string> items2 = new BindingList<string>();
            ListView mainList2 = (ListView)FindViewById<ListView>(Resource.Id.mainlistview2);

            foreach(Entry i in root.entries)
            {
                items2.Add(i.name);
            }
            ArrayAdapter adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, items2);
            mainList2.Adapter = adapter;
            mainList2.ItemClick += (s, e) => {
                var t = items2[e.Position];
                string itemSelected = items2[e.Position];
                foreach (Entry i in root.entries)
                {
                    if (itemSelected == i.name)
                    {
                        alert.SetMessage(i.regulation);
                        alert.Show();
                    }
                }
            };
            SearchView search = FindViewById<SearchView>(Resource.Id.search);
            search.SetQueryHint("Search for a food!");
            search.QueryTextChange += sv_QueryTextChange;
            void sv_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
            {
                firstLayout.Visibility = ViewStates.Gone;
                secondLayout.Visibility = ViewStates.Gone;
                firstClick.Visibility = ViewStates.Gone;
                secondClick.Visibility = ViewStates.Gone;
                thirdClick.Visibility = ViewStates.Gone;
                fourthClick.Visibility = ViewStates.Gone;
                listLayout2.Visibility = ViewStates.Gone;
                listLayout2.Visibility = ViewStates.Visible;
                adapter.Filter.InvokeFilter(e.NewText);
                currentLayout = 13;
            }

            //Click events for each button
            firstButton.Click += delegate
            {
                firstLayout.Visibility = ViewStates.Gone;
                secondLayout.Visibility = ViewStates.Gone;
                firstClick.Visibility = ViewStates.Visible;
                currentLayout = 1;
            };
            secondButton.Click += delegate
            {
                firstLayout.Visibility = ViewStates.Gone;
                secondLayout.Visibility = ViewStates.Gone;
                secondClick.Visibility = ViewStates.Visible;
                currentLayout = 2;
            };
            thirdButton.Click += delegate
            {
                firstLayout.Visibility = ViewStates.Gone;
                secondLayout.Visibility = ViewStates.Gone;
                thirdClick.Visibility = ViewStates.Visible;
                currentLayout = 3;
            };
            fourthButton.Click += delegate
            {
                firstLayout.Visibility = ViewStates.Gone;
                secondLayout.Visibility = ViewStates.Gone;
                fourthClick.Visibility = ViewStates.Visible;
                currentLayout = 4;
            };
            fifthButton.Click += delegate
            {
                currentLayout = 5;
                FiveSixSevenEightListing("Refridgerated/FrozenProcessed");
            };
            sixthButton.Click += delegate
            {
                currentLayout = 6;
                FiveSixSevenEightListing("FreshProduce");
            };
            seventhButton.Click += delegate
            {
                currentLayout = 7;
                FiveSixSevenEightListing("SamplingOfFoodProducts");
            };
            eighthButton.Click += delegate
            {
                currentLayout = 8;
                FiveSixSevenEightListing("OtherFoodProducts");
            };
            meatButton.Click += delegate
            {
                fourthClick.Visibility = ViewStates.Gone;
                currentLayout = 9;
                SubCategoryListing("Meat");
            };
            eggsButton.Click += delegate
            {
                fourthClick.Visibility = ViewStates.Gone;
                currentLayout = 9;
                SubCategoryListing("Eggs");
            };
            dairyButton.Click += delegate
            {
                fourthClick.Visibility = ViewStates.Gone;
                currentLayout = 9;
                SubCategoryListing("DairyProducts");
            };
            bakedButton.Click += delegate
            {
                firstClick.Visibility = ViewStates.Gone;
                currentLayout = 10;
                SubCategoryListing("BakedGoods");
            };
            preparedButton.Click += delegate
            {
                firstClick.Visibility = ViewStates.Gone;
                currentLayout = 10;
                SubCategoryListing("PreparedFoods");
            };
            alcButton.Click += delegate
            {
                thirdClick.Visibility = ViewStates.Gone;
                currentLayout = 11;
                SubCategoryListing("Alcoholic");
            };
            nonAlcButton.Click += delegate
            {
                thirdClick.Visibility = ViewStates.Gone;
                currentLayout = 11;
                SubCategoryListing("Non-Alcoholic");
            };
            jamButton.Click += delegate
            {
                secondClick.Visibility = ViewStates.Gone;
                currentLayout = 12;
                SubCategoryListing("Jams");
            };
            jellyButton.Click += delegate
            {
                secondClick.Visibility = ViewStates.Gone;
                currentLayout = 12;
                SubCategoryListing("Jellies");
            };
            shelfStableButton.Click += delegate
            {
                secondClick.Visibility = ViewStates.Gone;
                currentLayout = 12;
                SubCategoryListing("ShelfStableFood");
            };
            void FiveSixSevenEightListing(string foodItem)
            {
                firstLayout.Visibility = ViewStates.Gone;
                secondLayout.Visibility = ViewStates.Gone;
                foreach(Category i in root.categories)
                {
                    if(i.categoryId == foodItem)
                    {
                        foreach(Entry j in root.entries)
                        {
                            for(int k = 0; k < i.children.Count; k++)
                            {
                                if(j.entryId == i.children[k])
                                {
                                    items.Add(j.name);
                                }
                            }
                        }
                    }
                }
                mainList.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, items);
                listLayout.Visibility = ViewStates.Visible;
            }

            void SubCategoryListing(string foodItem)
            {
                firstLayout.Visibility = ViewStates.Gone;
                secondLayout.Visibility = ViewStates.Gone;
                foreach (Subcategory i in root.subcategories)
                {
                    if (i.subcategoryId == foodItem)
                    {
                        foreach (Entry j in root.entries)
                        {
                            for (int k = 0; k < i.children.Count; k++)
                            {
                                if (j.entryId == i.children[k])
                                {
                                    items.Add(j.name);
                                }
                            }
                        }
                    }
                }
                mainList.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, items);
                listLayout.Visibility = ViewStates.Visible;
            }

            //Handles the back button, showing and 
            backButton.Click += delegate
            {
                if(currentLayout != 0)
                {
                    switch (currentLayout)
                    {
                        case 1:
                            firstClick.Visibility = ViewStates.Gone;
                            firstLayout.Visibility = ViewStates.Visible;
                            secondLayout.Visibility = ViewStates.Visible;
                            break;
                        case 2:
                            secondClick.Visibility = ViewStates.Gone;
                            firstLayout.Visibility = ViewStates.Visible;
                            secondLayout.Visibility = ViewStates.Visible;
                            break;
                        case 3:
                            thirdClick.Visibility = ViewStates.Gone;
                            firstLayout.Visibility = ViewStates.Visible;
                            secondLayout.Visibility = ViewStates.Visible;
                            break;
                        case 4:
                            fourthClick.Visibility = ViewStates.Gone;
                            firstLayout.Visibility = ViewStates.Visible;
                            secondLayout.Visibility = ViewStates.Visible;
                            break;
                        case 5:
                            listLayout.Visibility = ViewStates.Gone;
                            firstLayout.Visibility = ViewStates.Visible;
                            secondLayout.Visibility = ViewStates.Visible;
                            items.Clear();
                            break;
                        case 6:
                            listLayout.Visibility = ViewStates.Gone;
                            firstLayout.Visibility = ViewStates.Visible;
                            secondLayout.Visibility = ViewStates.Visible;
                            items.Clear();
                            break;
                        case 7:
                            listLayout.Visibility = ViewStates.Gone;
                            firstLayout.Visibility = ViewStates.Visible;
                            secondLayout.Visibility = ViewStates.Visible;
                            items.Clear();
                            break;
                        case 8:
                            listLayout.Visibility = ViewStates.Gone;
                            firstLayout.Visibility = ViewStates.Visible;
                            secondLayout.Visibility = ViewStates.Visible;
                            items.Clear();
                            break;
                        //meat, Eggs, and Dairy
                        case 9:
                            listLayout.Visibility = ViewStates.Gone;
                            fourthClick.Visibility = ViewStates.Visible;
                            items.Clear();
                            currentLayout = 4;
                            break;
                        //baked, prepared, imcomp
                        case 10:
                            listLayout.Visibility = ViewStates.Gone;
                            firstClick.Visibility = ViewStates.Visible;
                            items.Clear();
                            currentLayout = 1;
                            break;
                        //alc and non alc
                        case 11:
                            listLayout.Visibility = ViewStates.Gone;
                            thirdClick.Visibility = ViewStates.Visible;
                            items.Clear();
                            currentLayout = 3;
                            break;
                        case 12:
                            listLayout.Visibility = ViewStates.Gone;
                            secondClick.Visibility = ViewStates.Visible;
                            items.Clear();
                            currentLayout = 2;
                            break;
                        case 13:
                            search.SetQuery("", true);
                            search.SetIconifiedByDefault(true);
                            firstLayout.Visibility = ViewStates.Visible;
                            secondLayout.Visibility = ViewStates.Visible;
                            firstClick.Visibility = ViewStates.Gone;
                            secondClick.Visibility = ViewStates.Gone;
                            thirdClick.Visibility = ViewStates.Gone;
                            fourthClick.Visibility = ViewStates.Gone;
                            listLayout2.Visibility = ViewStates.Gone;
                            search.SetIconifiedByDefault(false);
                            search.ClearFocus();
                            break;
                    }
                }
            };
        }
        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if(drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            //Declarations for sets of buttons
            int id = item.ItemId;
            if (id == Resource.Id.nav_new)
            {
            }
            else if (id == Resource.Id.nav_markets)
            {
                var uri = Android.Net.Uri.Parse("https://www.fromthelandofkansas.com/market/list");
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);
            }
            else if (id == Resource.Id.nav_report)
            {
            }

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}

