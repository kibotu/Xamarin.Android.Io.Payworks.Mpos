using Android.App;
using Android.Widget;
using Android.OS;
using IO.Mpos.UI.Shared;
using IO.Mpos.Provider;
using IO.Mpos.UI.Shared.Model;
using IO.Mpos.Accessories.Parameters;
using IO.Mpos.Accessories;
using IO.Mpos.Transactions.Parameters;
using Java.Math;
using Java.Util;
using Android.Content;
using Android.Util;

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

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            Log.Verbose("Demo", $"[OnActivityResult] {requestCode} {resultCode} {data}");
            if (requestCode == MposUi.RequestCodePayment)
                {
                //if (resultCode == MposUi.ResultCodeApproved)
                    //{
                        // Transaction was approved
                Toast.MakeText(this, $"Transaction {resultCode}", ToastLength.Long).Show();
                    //}
                    //else
                    //{
                    // Card was declined, or transaction was aborted, or failed
                    // (e.g. no internet or accessory not found)
                    //Toast.MakeText(this, "Transaction was declined, aborted, or failed", ToastLength.Long).Show();
                    //}
                    // Grab the processed transaction in case you need it
                    // (e.g. the transaction identifier for a refund).
                    // Keep in mind that the returned transaction might be null
                    // (e.g. if it could not be registered).
                     var transaction = MposUi.InitializedInstance.Transaction;
                }
        }

    }

}