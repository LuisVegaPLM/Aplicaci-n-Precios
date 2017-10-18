select pbc.PresentationId, pbc.BarCodeId,pbc.LastUpdate,bc.BarCode,pp.Price, pp.LastUpdate,ps.PriceSourceId,ps.SourceName
 from ProductBarCodes pbc inner join BarCodes bc on pbc.BarCodeId = bc.BarCodeId 
                                  inner join ProductPrices pp on pbc.BarCodeId = pp.BarCodeId and pbc.PresentationId = pp.PresentationId
                                  inner join PriceSources ps on pp.PriceSourceId = ps.PriceSourceId
where pbc.BarCodeId in (316,4470,9451,3525,1576,2764,155,3952,406,3956,9234,3826,3827,4276,3898,6475,1688,5141,2587,8863,
994,5538,1748,8704,993,1367,1448,1451,7164,7166,1011,1014,1046,8391,9606,8392,5460,9085,2335,5266,2300,5158,5237,2342,7324,
1494,1153,1582,1617,1584,1583,1586,4137,6512,4144,4152,9422,2751,5338,6499
) and ps.PriceSourceId >11


select * from HistoricProductPrices

select * from PriceSources



select pbc.PresentationId, pbc.BarCodeId,pbc.LastUpdate,bc.BarCode,pp.Price, pp.LastUpdate,ps.PriceSourceId,ps.SourceName
 from ProductBarCodes pbc inner join BarCodes bc on pbc.BarCodeId = bc.BarCodeId 
                                  inner join ProductPrices pp on pbc.BarCodeId = pp.BarCodeId and pbc.PresentationId = pp.PresentationId
                                  inner join PriceSources ps on pp.PriceSourceId = ps.PriceSourceId


Select top 10 * from Presentations p inner join ProductBarCodes pb on p.ProductId =pb.PresentationId
                              inner join BarCodes b on pb.BarCodeId = b.BarCodeId


select distinct b.PresentationId, b.Divisionid, b.CategoryId, b.ProductId,b.PharmaformId,b.BarCodeId, b.NombreProducto,
                 p.Brand,  pf.Description, ps.Presentation
 from ByPrices b left join ProductPharmaForms ppf on b.PharmaformId = ppf.PharmaFormId and b.productId = ppf.ProductId
                 left join PharmaceuticalForms pf on ppf.PharmaFormId = pf.PharmaFormId
                 left join Products p on ppf.ProductId = p.ProductId
				 left join Presentations ps on b.PresentationId = ps.PresentationId and
				                               b.Divisionid = ps.DivisionId and
											   b.CategoryId = ps.CategoryId and
											   b.ProductId= ps.ProductId  and
											   b.PharmaformId = ps.PharmaFormId
order by p.Brand, 1,2,3,4,5,6



select * from BarCodes where BarCodeId = 1748