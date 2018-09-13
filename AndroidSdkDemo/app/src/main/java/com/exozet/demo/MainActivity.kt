package com.exozet.demo

import android.content.Intent
import android.os.Bundle
import android.support.v7.app.AppCompatActivity
import android.widget.Toast
import io.mpos.accessories.AccessoryFamily
import io.mpos.accessories.parameters.AccessoryParameters
import io.mpos.provider.ProviderMode
import io.mpos.transactions.parameters.TransactionParameters
import io.mpos.ui.shared.MposUi
import io.mpos.ui.shared.model.MposUiConfiguration
import java.math.BigDecimal
import java.util.*


class MainActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        // http://www.payworks.mpymnt.com/node/143
        paymentButtonClicked()
    }

    fun paymentButtonClicked() {
        val ui = MposUi.initialize(this, ProviderMode.MOCK,
                "merchantIdentifier", "merchantSecretKey")

        ui.configuration.summaryFeatures = EnumSet.of(
                // Add this line, if you do want to offer printed receipts
                // MposUiConfiguration.SummaryFeature.PRINT_RECEIPT,
                MposUiConfiguration.SummaryFeature.SEND_RECEIPT_VIA_EMAIL)

        // Start with a mocked card reader:
        val accessoryParameters = AccessoryParameters.Builder(AccessoryFamily.MOCK)
                .mocked()
                .build()
        ui.configuration.terminalParameters = accessoryParameters

        val transactionParameters = TransactionParameters.Builder()
                .charge(BigDecimal("5.00"), io.mpos.transactions.Currency.EUR)
                .subject("Bouquet of Flowers")
                .customIdentifier("yourReferenceForTheTransaction")
                .build()

        val intent = ui.createTransactionIntent(transactionParameters)
        startActivityForResult(intent, MposUi.REQUEST_CODE_PAYMENT)
    }

    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent) {

        if (requestCode == MposUi.REQUEST_CODE_PAYMENT) {
            if (resultCode == MposUi.RESULT_CODE_APPROVED) {
                // Transaction was approved
                Toast.makeText(this, "Transaction approved", Toast.LENGTH_LONG).show()
            } else {
                // Card was declined, or transaction was aborted, or failed
                // (e.g. no internet or accessory not found)
                Toast.makeText(this, "Transaction was declined, aborted, or failed",
                        Toast.LENGTH_LONG).show()
            }
            // Grab the processed transaction in case you need it
            // (e.g. the transaction identifier for a refund).
            // Keep in mind that the returned transaction might be null
            // (e.g. if it could not be registered).
            val transaction = MposUi.getInitializedInstance().transaction
        }
    }
}
