using System;
using System.Diagnostics;
using PanCardView.Processors;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HiAnalyticsDemo
{
    public partial class App : Application
    {
        public static string _pageName = "App";

        public App ()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }

        public static string FirebaseLimit(string text)
        {
            try
            {
                return LimitString(text, 100);
            }
            catch (Exception ex)
            {
                ExceptionDescription(_pageName, "FirebaseLimit", ex);
            }
            return text;
        }

        public static string LimitString(string data, int limit)
        {
            try
            {
                if (data.Length > limit)
                    data = data.Remove(limit);
                return data;
            }
            catch (Exception ex)
            {
                ExceptionDescription(_pageName, "LimitString", ex);
                return data;
            }
        }

        public static void ExceptionDescription(string page, string command, Exception error)
        {
            string datetime = DateTime.Now.ToLongTimeString();
            try
            {
                Debug.WriteLine($"{datetime} : '{page}' Error Handler for '{command}' : {error.ToString()}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{datetime} : '{_pageName}' Error Handler for 'ExceptionDescription' : {ex.ToString()}");
            }
        }
    }
}

