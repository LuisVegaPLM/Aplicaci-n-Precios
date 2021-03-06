USE [Medinet 20170721]
GO
/****** Object:  StoredProcedure [dbo].[plm_spUpdatePriceByProductBarCodeByPriceSource]    Script Date: 07/08/2017 9:47:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* 
	Author:			Luis Vega.				 
	Object:			dbo.plm_spUpdatePriceByProductBarCodeByPriceSource
	
	EXEC dbo.plm_spUpdatePriceByProductBarCodeByPriceSource @BarCodeId = 316, @PresentationId= 1, @Price  = 302.0, @SourceName = 'Comercial Mexicana'
 */ 

ALTER PROCEDURE [dbo].[plm_spUpdatePriceByProductBarCodeByPriceSource]
(
	@BarCodeId				int,
	@PresentationId		    int,
	@Price                  float,
	@SourceName             varchar(250)
)
AS
BEGIN
BEGIN TRAN
 DECLARE @PriceSourceId int

 IF NOT EXISTS (SELECT * FROM PriceSources WHERE SourceName=@SourceName)
   BEGIN
       SELECT @PriceSourceId = (SELECT MAX(PriceSourceId) FROM PriceSources ) + 1

       INSERT INTO [dbo].[PriceSources]
	                     (PriceSourceId,SourceName,SourceShortName,Active)  
	   VALUES (@PriceSourceId,@SourceName,@SourceName,1)
   END 
 ELSE
   BEGIN
	  SELECT @PriceSourceId = (SELECT DISTINCT PriceSourceId FROM PriceSources WHERE SourceName=@SourceName )
   END 	
          
 IF NOT EXISTS (SELECT * FROM ProductPrices WHERE PresentationId = @PresentationId and BarCodeId = @BarCodeId and PriceSourceId = @PriceSourceId)     
   BEGIN
      INSERT INTO [dbo].[ProductPrices]
	                    (PresentationId,BarCodeId,PriceSourceId,Price,LastUpdate)
	  VALUES (@PresentationId,@BarCodeId,@PriceSourceId,@Price,GETDATE())
   END           
  
 ELSE
   BEGIN
     UPDATE [dbo].[ProductPrices]
	   SET Price = @Price
	 WHERE PresentationId = @PresentationId and
	       BarCodeId = @BarCodeId and
		   PriceSourceId = @PriceSourceId
   END    
  
  INSERT INTO [dbo].[HistoricProductPrices]
                    (PresentationId,BarCodeId,PriceSourceId,AddedDate,Price)
  VALUES (@PresentationId,@BarCodeId,@PriceSourceId,GETDATE(),@Price)					               
                    
COMMIT   
RETURN 1            
END
