#!/usr/bin/env bash

rm -rf Demo/obj
rm -rf Demo/bin

rm -rf Xamarin.Io.Payworks.Mpos.Android.Core/obj
rm -rf Xamarin.Io.Payworks.Mpos.Android.Core/bin

rm -rf Xamarin.Io.Payworks.Mpos.Android.Ui/obj
rm -rf Xamarin.Io.Payworks.Mpos.Android.Ui/bin

rm -rf Xamarin.Io.Payworks.Mpos.Core/obj
rm -rf Xamarin.Io.Payworks.Mpos.Core/bin

nuget restore
