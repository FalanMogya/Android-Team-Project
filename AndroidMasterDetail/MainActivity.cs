using System;
using System.Collections;
using Android;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace AndroidMasterDetail
{
    [Activity(Label = "KSRE", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
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
            //Declarations for buttons
            Button firstButton = FindViewById<Button>(Resource.Id.firstButton);
            Button secondButton = FindViewById<Button>(Resource.Id.secondButton);
            Button thirdButton = FindViewById<Button>(Resource.Id.thirdButton);
            Button fourthButton = FindViewById<Button>(Resource.Id.fourthButton);
            Button fifthButton = FindViewById<Button>(Resource.Id.fifthButton);
            Button sixthButton = FindViewById<Button>(Resource.Id.sixthButton);
            Button backButton = FindViewById<Button>(Resource.Id.backButton);

            //Declarations for sets of buttons
            LinearLayout firstLayout = FindViewById<LinearLayout>(Resource.Id.leftCol);
            LinearLayout secondLayout = FindViewById<LinearLayout>(Resource.Id.rightCol);
            LinearLayout firstClick = FindViewById<LinearLayout>(Resource.Id.firstClick);
            LinearLayout secondClick = FindViewById<LinearLayout>(Resource.Id.secondClick);
            LinearLayout thirdClick = FindViewById<LinearLayout>(Resource.Id.thirdClick);
            LinearLayout fourthClick = FindViewById<LinearLayout>(Resource.Id.fourthClick);
            LinearLayout fifthClick = FindViewById<LinearLayout>(Resource.Id.fifthClick);
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
                firstLayout.Visibility = ViewStates.Gone;
                secondLayout.Visibility = ViewStates.Gone;
                fifthClick.Visibility = ViewStates.Visible;
                currentLayout = 5;
            };
            backButton.Click += delegate
            {
                if(currentLayout != 0)
                {
                    firstLayout.Visibility = ViewStates.Visible;
                    secondLayout.Visibility = ViewStates.Visible;
                    switch (currentLayout)
                    {
                        case 1:
                            firstClick.Visibility = ViewStates.Invisible;
                            break;
                        case 2:
                            secondClick.Visibility = ViewStates.Invisible;
                            break;
                        case 3:
                            thirdClick.Visibility = ViewStates.Invisible;
                            break;
                        case 4:
                            fourthClick.Visibility = ViewStates.Invisible;
                            break;
                        case 5:
                            fifthClick.Visibility = ViewStates.Invisible;
                            break;
                    }
                }
            };
            //RelativeLayout bugReporting = FindViewById<RelativeLayout>(Resource.Id.bugreporting);
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
            int id = item.ItemId;
            if (id == Resource.Id.nav_new)
            {
                // Handle the camera action
            }
            else if (id == Resource.Id.nav_markets)
            {

            }
            else if (id == Resource.Id.nav_report)
            {
                SetContentView(Resource.Layout.bugreporting);
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

