Domain Model Notes
1. Assumed that a tax or price will have to be rounded of to 0.05 only if there are more than 2 decimal places for such a number. 
2. Tax calculation is decoupled from Product price and quantity using a TaxCategory mapping. This is to simulate real world situations where taxation on products vary from time to time.
3. Product and it's packaging are decouple to accomodate real world situations eg. a item is priced in grams but sold in a pack of minimum 2 kgs or for situations like product is priced on per unit but sold in sealed packets of x units. 
4. The taxation, packaging and product are loosely coupled to provide mix and match options in creating and maintaining a product master listing.

Miscellaneous / Testing 
- Run InsightInvoice.Output project. It will display expected outputs in a console app. A verbose option has been provided to show additional information like gross price and taxation per product added to invoice.
- XML comments not applied to classes and methods for compactness. Most of things are self-explanatory. 
- The goal of test coverage is primarily to calculate taxes, net and gross prices close on real world packaging possibilities. 
- Not all classes and properties unit tested to keep primary focus on calculation of prices and taxes. 
- The three sample inovice inputs are divided into multiple sub unit tests. Each product applied in sample invoice has it's taxation and net price unit tested to increase test coverage. 
- Inovice output display formatting not unit tested assuming it's limited role as logging output.