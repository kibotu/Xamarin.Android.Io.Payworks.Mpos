using Android.App;
using Android.Widget;
using Android.OS;
using System;
using IO.Mpos.UI.Shared;
using IO.Mpos.Provider;
using IO.Mpos.UI.Shared.Model;
using IO.Mpos.Accessories.Parameters;
using IO.Mpos.Accessories;
using IO.Mpos.Transactions.Parameters;
using Java.Math;
using Java.Util;
using Android.Content;

namespace Demo2
{
    [Activity(Label = "Demo2", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            Button button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += delegate { Pay(); };
        }

        private void Pay()
        {
            MposUi ui = MposUi.Initialize(this, ProviderMode.Mock, "merchantIdentifier", "merchantSecretKey");
            ui.Configuration.SetSummaryFeatures(EnumSet.Of(
                // Add this line, if you do want to offer printed receipts
                // MposUiConfiguration.SummaryFeature.PRINT_RECEIPT,
                MposUiConfiguration.SummaryFeature.SendReceiptViaEmail)
            );

            //// Start with a mocked card reader:
            AccessoryParameters accessoryParameters = new AccessoryParameters.Builder(AccessoryFamily.Mock)
                .Mocked()
                .Build();
            ui.Configuration.SetTerminalParameters(accessoryParameters);

            var transactionParameters = new TransactionParametersBuilder()
                .Charge(new BigDecimal("5.00"), IO.Mpos.Transactions.Currency.Eur)
                .Subject("Bouquet of Flowers")
                .CustomIdentifier("yourReferenceForTheTransaction")
                .Build();

            Intent intent = ui.CreateTransactionIntent(transactionParameters);
            StartActivityForResult(intent, MposUi.RequestCodePayment);
        }
    }

}