# DXT_PaymentMatcher
DXT Task

N.B. this version works with the purchase.dat file you sent me attached, the specification in the PDF file describe a different format for the purchase.dat (leading zeros, newline after item) .
This version does NOT work with the file described in the specifications. if you want it to work with the file format you specified you need to open PurchaseReaderService.cs and substitute the blocks with the commented code above them.

Put the Data folder with the Payments.Json, Prices.XML and Purchase.dat in the same directory of  PaymentMatcher.exe
To run type : PaymentMatcher.exe <output file format>  ex: PaymentMatcher.exe csv 
if the format is not specified the output will be in Json
the output will be in the same directory of the .exe and called PaymentsNotMatched.Json (or csv or html)


The program has been done in c# 8 on .Net Framework the IDE is VS2019
