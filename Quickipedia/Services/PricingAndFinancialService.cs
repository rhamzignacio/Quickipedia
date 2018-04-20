using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quickipedia.Models;
using System.Data.Entity;

namespace Quickipedia.Services
{
    public class PricingAndFinancialService
    {
        //AdminFee
        public static EcardAdminFeeModel GetAdminFee(out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var query = from a in db.EcardAdminFee
                                join u in db.UserAccount on a.ModifiedBy equals u.ID into qU
                                from us in qU.DefaultIfEmpty()
                                where a.ClientCode == UniversalHelpers.SelectedClient
                                select new EcardAdminFeeModel
                                {
                                    ClientCode = a.ClientCode,
                                    AirFare = a.AirFareFlag,
                                    Divide  = a.Divide,
                                    ModifiedBy  = a.ModifiedBy,
                                    ModifiedDate = a.ModifiedDate,
                                    Multiply = a.Multiply,
                                    Others = a.OtherFeeFlag,
                                    ServiceFee = a.ServiceFeeFlag,
                                    ShowModifiedBy = us.FirstName + " " + us.LastName
                                };

                    if (query.FirstOrDefault() != null)
                        return query.FirstOrDefault();
                    else
                        return new EcardAdminFeeModel();
                }
            }
            catch (Exception error)
            {
                message = error.Message;

                return new EcardAdminFeeModel(); ;
            }
        }

        public static void SaveAdminFee(EcardAdminFeeModel _model, out string message)
        {
            try
            {
                message = "Saved";

                using (var db = new QuickipediaEntities())
                {
                    var admin = db.EcardAdminFee.FirstOrDefault(r => r.ClientCode == UniversalHelpers.SelectedClient);

                    if (admin == null)//new
                    {
                        EcardAdminFee newAdmin = new EcardAdminFee
                        {
                            ClientCode = UniversalHelpers.SelectedClient,
                            AirFareFlag = _model.AirFare,
                            Divide = _model.Divide,
                            ModifiedBy = UniversalHelpers.CurrentUser.ID,
                            ModifiedDate = DateTime.Now,
                            Multiply = _model.Multiply,
                            OtherFeeFlag = _model.Others,
                            ServiceFeeFlag = _model.ServiceFee
                        };

                        db.Entry(newAdmin).State = EntityState.Added;
                    }
                    else//update
                    {
                        admin.AirFareFlag = _model.AirFare;

                        admin.Divide = _model.Divide;

                        admin.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                        admin.ModifiedDate = DateTime.Now;

                        admin.Multiply = _model.Multiply;

                        admin.OtherFeeFlag = _model.Others;

                        admin.ServiceFeeFlag = _model.ServiceFee;

                        db.Entry(admin).State = EntityState.Modified;
                    }

                    db.SaveChanges();
                }
            }
            catch (Exception error)
            {
                message = error.Message;
            }
        }

        //FareComparison
        public static FareComparisonModel GetFareComparison(out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var query = from f in db.FareComparison
                                join u in db.UserAccount on f.ModifiedBy equals u.ID into qU
                                from us in qU.DefaultIfEmpty()
                                where f.ClientCode == UniversalHelpers.SelectedClient
                                select new FareComparisonModel
                                {
                                    ID = f.ID,
                                    ClientCode = f.ClientCode,
                                    CarStandardFare = f.CarStandardFare,
                                    HotelStandardFare = f.HotelStandardFare,
                                    LF = f.LF,
                                    LowFare = f.LowFare,
                                    RF = f.RF,
                                    ReferenceFare = f.ReferenceFare,
                                    ModifiedBy = f.ModifiedBy,
                                    ShowModifiedBy = us.FirstName + " " + us.LastName,
                                    ModifiedDate = f.ModifiedDate
                                };

                    if (query.FirstOrDefault() != null)
                        return query.FirstOrDefault();
                    else
                        return new FareComparisonModel();
                }
            }
            catch(Exception error)
            {
                message = error.Message;

                return null;
            }
        }

        public static void SaveFareComparison(FareComparisonModel _fare, out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var fare = db.FareComparison.FirstOrDefault(r => r.ClientCode == UniversalHelpers.SelectedClient);

                    if(fare != null)
                    {
                        fare.LF = _fare.LF;

                        fare.LowFare = _fare.LowFare;

                        fare.RF = _fare.RF;

                        fare.ReferenceFare = _fare.ReferenceFare;

                        fare.HotelStandardFare = _fare.HotelStandardFare;

                        fare.CarStandardFare = _fare.CarStandardFare;

                        fare.ModifiedDate = DateTime.Now;

                        fare.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                        db.Entry(fare).State = EntityState.Modified;

                        message = "Updated";
                    }
                    else
                    {
                        FareComparison newFare = new FareComparison
                        {
                            ID = Guid.NewGuid(),
                            ClientCode = UniversalHelpers.SelectedClient,
                            LF = _fare.LF,
                            LowFare = _fare.LowFare,
                            RF = _fare.RF,
                            ReferenceFare = _fare.ReferenceFare,
                            CarStandardFare = _fare.CarStandardFare,
                            HotelStandardFare = _fare.HotelStandardFare,
                            ModifiedBy = UniversalHelpers.CurrentUser.ID,
                            ModifiedDate = DateTime.Now
                        };

                        db.Entry(newFare).State = EntityState.Added;

                        message = "Saved";
                    }

                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static List<TableOfFeesCategoryDropDown> GetCategoryDropDown(out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var query = from c in db.TableOfFeesCategory
                                orderby c.CategoryName
                                select new TableOfFeesCategoryDropDown
                                {
                                    ID = c.ID,
                                    Category = c.CategoryName
                                };

                    return query.ToList();
                }
            }
            catch(Exception error)
            {
                message = error.Message;

                return null;
            }
        }

        public static List<TableOfFeesCategoryModel> GetTableOfFeesCategory(out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var query = from c in db.TableOfFeesCategory
                                join u in db.UserAccount on c.ModifiedBy equals u.ID
                                orderby c.ArrangeBy, c.CategoryName ascending
                                select new TableOfFeesCategoryModel
                                {
                                    ID = c.ID,
                                    CategoryName = c.CategoryName,
                                    ModifiedBy = u.ModifiedBy,
                                    ShowModifiedBy = u.FirstName + " " + u.LastName,
                                    ModifiedDate = c.ModifiedDate,
                                    ArrangeBy = c.ArrangeBy
                                };

                    return query.ToList();
                }
            }
            catch(Exception error)
            {
                message = error.Message;

                return null;
            }
        }

        public static void SaveTableOfFeesCategory(TableOfFeesCategoryModel model, out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var category = db.TableOfFeesCategory.FirstOrDefault(r => r.ID == model.ID);

                    if(category != null)//UPDATE
                    {

                        if(category != null)
                        {
                            if (model.Status == "X")
                            {
                                message = "Deleted";

                                db.Entry(category).State = EntityState.Deleted;
                            }
                            else
                            {
                                message = "Updated";

                                category.CategoryName = model.CategoryName;

                                category.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                                category.ModifiedDate = DateTime.Now;

                                category.ArrangeBy = model.ArrangeBy;

                                db.Entry(category).State = EntityState.Modified;
                            }
                        }
                    }
                    else
                    {
                        message = "Saved";

                        var ifExist = db.TableOfFeesCategory.FirstOrDefault(r => r.CategoryName.ToLower() == model.CategoryName.ToLower());

                        if (ifExist != null)
                            message = "Category already exist";
                        else
                        {
                            TableOfFeesCategory newCategory = new TableOfFeesCategory
                            {
                                ID = Guid.NewGuid(),
                                CategoryName = model.CategoryName,
                                ModifiedBy = UniversalHelpers.CurrentUser.ID,
                                ModifiedDate = DateTime.Now,
                                ArrangeBy = model.ArrangeBy
                            };

                            db.Entry(newCategory).State = EntityState.Added;
                        }
                    }

                    db.SaveChanges();
                }
            }
            catch (Exception error)
            {
                message = error.Message;
            }
        }

        public static void SaveOther(OtherPricingAndFinancialModel model, out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var other = db.OtherPricingAndFinancial.FirstOrDefault(r => r.ClientCode == UniversalHelpers.SelectedClient);

                    if(other != null) //UPDATE
                    {
                        message = "Updated";

                        other.Value = model.Value;

                        other.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                        other.ModifiedDate = DateTime.Now;

                        db.Entry(other).State = EntityState.Modified;
                    }
                    else
                    {
                        message = "Saved";

                        OtherPricingAndFinancial newOther = new OtherPricingAndFinancial
                        {
                            ID = Guid.NewGuid(),
                            Value = model.Value,
                            ClientCode = UniversalHelpers.SelectedClient,
                            ModifiedBy = UniversalHelpers.CurrentUser.ID,
                            ModifiedDate = DateTime.Now
                        };

                        db.Entry(newOther).State = EntityState.Added;
                    }

                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static void SaveInvoiceAttachment(InvoiceAttachmentModel model, out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                        var inv = db.InvoiceAttachment.FirstOrDefault(r => r.ClientCode == UniversalHelpers.SelectedClient);

                        if(inv != null)//UPDATE
                        {
                            message = "Updated";

                            inv.Value = model.Value;

                            inv.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                            inv.ModifiedDate = DateTime.Now;

                            db.Entry(inv).State = EntityState.Modified;
                        }
                        else
                        {
                            message = "Saved";

                            InvoiceAttachment newInv = new InvoiceAttachment
                            {
                                ID = Guid.NewGuid(),
                                Value = model.Value,
                                ClientCode = UniversalHelpers.SelectedClient,
                                ModifiedBy = UniversalHelpers.CurrentUser.ID,
                                ModifiedDate = DateTime.Now
                            };

                            db.Entry(newInv).State = EntityState.Added;
                        }

                        db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static void SaveTableOfFees(TableOfFeesModels model, out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    if(model.ID != Guid.Empty) // UPDATE
                    {
                        var fee = db.TableOfFees.FirstOrDefault(r => r.ID == model.ID);

                        if(fee != null)
                        {
                            if (model.Status == "U")
                            {
                                message = "Updated";

                                fee.CategoryID = model.CategoryID;

                                fee.Description = model.Description;

                                fee.PHPMice = model.PHPMice;

                                fee.PHPNonGDS = model.PHPNonGDS;

                                fee.PHPOnline = model.PHPOnline;

                                fee.PHPTraditionalGDS = model.PHPTraditionalGDS;

                                fee.USDMice = model.USDMice;

                                fee.USDNonGDS = model.USDNonGDS;

                                fee.USDOnline = model.USDOnline;

                                fee.USDTraditionalGDS = model.USDTraditionalGDS;

                                fee.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                                fee.ModifiedDate = DateTime.Now;

                                db.Entry(fee).State = EntityState.Modified;
                            }
                            else if (model.Status == "X")
                            {
                                message = "Updated";

                                db.Entry(fee).State = EntityState.Deleted;
                            }
                        }
                    }
                    else //ADD
                    {
                        message = "Saved";

                        TableOfFees newFees = new TableOfFees
                            {
                                ID = Guid.NewGuid(),
                                ClientCode = UniversalHelpers.SelectedClient,
                                CategoryID = model.CategoryID,
                                Description = model.Description,
                                PHPMice = model.PHPMice,
                                PHPNonGDS = model.PHPNonGDS,
                                PHPOnline = model.PHPOnline,
                                PHPTraditionalGDS = model.PHPTraditionalGDS,
                                USDMice = model.USDMice,
                                USDNonGDS = model.USDNonGDS,
                                USDOnline = model.USDOnline,
                                USDTraditionalGDS = model.USDTraditionalGDS,
                                ModifiedDate = DateTime.Now,
                                ModifiedBy = UniversalHelpers.CurrentUser.ID
                            };

                        db.Entry(newFees).State = EntityState.Added;
                    }

                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static OtherPricingAndFinancialModel GetOther(out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var other = from o in db.OtherPricingAndFinancial
                                join u in db.UserAccount on o.ModifiedBy equals u.ID into qU
                                from user in qU.DefaultIfEmpty()
                                where o.ClientCode == UniversalHelpers.SelectedClient
                                select new OtherPricingAndFinancialModel
                                {
                                    ID = o.ID,
                                    ClientCode = o.ClientCode,
                                    Value = o.Value,
                                    ModifiedBy = o.ModifiedBy,
                                    ModifiedDate = o.ModifiedDate,
                                    ShowModifiedBy = user.FirstName + " " + user.LastName
                                };

                    if (other.FirstOrDefault() != null)
                        return other.FirstOrDefault();
                    else
                        return new OtherPricingAndFinancialModel();
                }
            }
            catch(Exception error)
            {
                message = error.Message;

                return new OtherPricingAndFinancialModel();
            }
        }

        public static List<TableOfFeesModels> GetTableOfFees (out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var fees = from f in db.TableOfFees
                               join u in db.UserAccount on f.ModifiedBy equals u.ID into qU
                               from user in qU.DefaultIfEmpty()
                               join c in db.TableOfFeesCategory on f.CategoryID equals c.ID into qC
                               from cat in qC.DefaultIfEmpty()
                               where f.ClientCode == UniversalHelpers.SelectedClient
                               select new TableOfFeesModels
                               {
                                   ID = f.ID,
                                   CategoryID = f.CategoryID,
                                   Category = cat.CategoryName,
                                   ClientCode = f.ClientCode,
                                   Description = f.Description,
                                   PHPMice = f.PHPMice,
                                   PHPNonGDS = f.PHPNonGDS,
                                   PHPOnline = f.PHPOnline,
                                   PHPTraditionalGDS = f.PHPTraditionalGDS,
                                   USDMice = f.USDMice,
                                   USDNonGDS = f.USDNonGDS,
                                   USDOnline = f.USDOnline,
                                   USDTraditionalGDS = f.USDTraditionalGDS,
                                   Status = "Y",
                                   ModifiedBy = f.ModifiedBy,
                                   ShowModifiedBy = user.FirstName + " " + user.LastName,
                                   ModifiedDate = f.ModifiedDate
                               };

                    return fees.ToList();
                }
            }
            catch(Exception error)
            {
                message = error.Message;

                return null;
            }
        }

        public static void SaveRefundProcess(RefundProcessModel refundProcess, out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var temp = db.RefundProcess.FirstOrDefault(r => r.ClientCode == UniversalHelpers.SelectedClient);

                    if(temp != null)//UPDATE
                    {
                        message = "Updated";

                        temp.RefundProcess1 = refundProcess.Process;

                        temp.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                        temp.ModifiedDate = DateTime.Now;

                        db.Entry(temp).State = EntityState.Modified;
                    }
                    else//NEW
                    {
                        message = "Saved";

                        RefundProcess newRefundProcess = new RefundProcess
                        {
                            ID = Guid.NewGuid(),
                            ClientCode = UniversalHelpers.SelectedClient,
                            RefundProcess1 = refundProcess.Process,
                            ModifiedBy = UniversalHelpers.CurrentUser.ID,
                            ModifiedDate = DateTime.Now
                        };

                        db.Entry(newRefundProcess).State = EntityState.Added;
                    }

                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static void SaveBillingCollection(BillingCollectionFinanceModel item, out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var temp = db.BillingCollectionFinanceManager.Where(r => r.ClientCode == UniversalHelpers.SelectedClient).ToList();

                    if (item.ID != Guid.Empty)
                    {
                        var ifExist = db.BillingCollectionFinanceManager.FirstOrDefault(r => r.ID == item.ID);

                        if (ifExist != null) //UPDATE
                        {
                            if (item.Status == "U")
                            {
                                ifExist.ContactNo = item.ContactNo;
                                ifExist.Email = item.Email;
                                ifExist.Name = item.Name;
                                ifExist.Position = item.Position;
                                ifExist.ModifiedDate = DateTime.Now;
                                ifExist.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                                db.Entry(ifExist).State = EntityState.Modified;

                                message = "Updated";
                            }
                            else
                            {
                                db.Entry(ifExist).State = EntityState.Deleted;

                                message = "Deleted";
                            }
                        }
                    }
                    else//NEW
                    {
                        BillingCollectionFinanceManager newBilling = new BillingCollectionFinanceManager
                        {
                            ID = Guid.NewGuid(),
                            ClientCode = UniversalHelpers.SelectedClient,
                            ContactNo = item.ContactNo,
                            Email = item.Email,
                            Name = item.Name,
                            Position = item.Position,
                            ModifiedBy = UniversalHelpers.CurrentUser.ID,
                            ModifiedDate = DateTime.Now
                        };

                        db.Entry(newBilling).State = EntityState.Added;

                        message = "Saved";
                    }
                    
                        db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static void SaveFormOfPayment(FormOfPaymentModel fop, out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var ifExist = db.FormOfPayment.FirstOrDefault(r => r.ClientCode == fop.ClientCode);

                    if(ifExist != null)
                    {
                        ifExist.CCLiquidationProcess = fop.CCLiquidationProcess;
                        ifExist.IssuedCard = fop.IssuedCard;
                        ifExist.TaxType = fop.TaxType;
                        ifExist.FOP = fop.FOP;
                        ifExist.ModifiedDate = DateTime.Now;
                        ifExist.ModifiedBy = UniversalHelpers.CurrentUser.ID;
                        ifExist.FOP_Others = fop.FOP_Others;
                        ifExist.IssuedCard_Others = fop.IssuedCard_Others;
                        ifExist.CCLiquidation_Others = fop.CCLiquidation_Others;
                        ifExist.MerchantFee = fop.MerchantFee;
                        ifExist.Currency = fop.Currency;
                        ifExist.Other_Currency = fop.OtherCurrency;

                        db.Entry(ifExist).State = EntityState.Modified;

                        message = "Updated";
                    }
                    else
                    {
                        FormOfPayment newFOP = new FormOfPayment
                        {
                            ID = Guid.NewGuid(),
                            FOP = fop.FOP,
                            CCLiquidationProcess = fop.CCLiquidationProcess,
                            ClientCode = UniversalHelpers.SelectedClient,
                            IssuedCard = fop.IssuedCard,
                            TaxType = fop.TaxType,
                            ModifiedBy = UniversalHelpers.CurrentUser.ID,
                            ModifiedDate = DateTime.Now,
                            FOP_Others = fop.FOP_Others,
                            CCLiquidation_Others = fop.CCLiquidation_Others,
                            IssuedCard_Others = fop.IssuedCard_Others,
                            MerchantFee = fop.MerchantFee,
                            Currency = fop.Currency,
                            Other_Currency = fop.OtherCurrency
                        };

                        db.Entry(newFOP).State = EntityState.Added;

                        message = "Saved";
                    }

                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static void SavePricingModel(PricingModels pricingModel, out string errorMessage)
        {
            try
            {
                using (var db = new QuickipediaEntities())
                {
                    errorMessage = "";

                    var ifExist = db.PricingModel.FirstOrDefault(r => r.ClientCode == pricingModel.ClientCode);

                    if(ifExist != null)
                    {
                        ifExist.BundleFlag = pricingModel.BundleFlag;
                        ifExist.RetainFlag = pricingModel.RetainFlag;
                        ifExist.ReturnFlag = pricingModel.ReturnFlag;
                        ifExist.UnbundledFlag = pricingModel.UnbundledFlag;
                        ifExist.ModifiedDate = DateTime.Now;
                        ifExist.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                        db.Entry(ifExist).State = EntityState.Modified;

                        errorMessage = "Updated";
                    }
                    else
                    {
                        PricingModel newPricingModel = new PricingModel
                        {
                            ID = Guid.NewGuid(),
                            ClientCode = UniversalHelpers.SelectedClient,
                            BundleFlag = pricingModel.BundleFlag,
                            RetainFlag = pricingModel.RetainFlag,
                            ReturnFlag = pricingModel.ReturnFlag,
                            UnbundledFlag = pricingModel.UnbundledFlag,
                            ModifiedBy = UniversalHelpers.CurrentUser.ID,
                            ModifiedDate = DateTime.Now
                        };

                        db.Entry(newPricingModel).State = EntityState.Added;

                        errorMessage = "Saved";             
                    }

                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                errorMessage = error.Message;
            }
        }

        public static InvoiceAttachmentModel GetInvoiceAttachment()
        {
            using (var db = new QuickipediaEntities())
            {
                var invs = from inv in db.InvoiceAttachment
                          join c in db.ClientProfile on inv.ClientCode equals c.ClientCode
                          join u in db.UserAccount on inv.ModifiedBy equals u.ID into qU
                          from user in qU.DefaultIfEmpty()
                          where inv.ClientCode == UniversalHelpers.SelectedClient
                          select new InvoiceAttachmentModel
                          {
                              ID = inv.ID,
                              Value = inv.Value,
                              ModifiedBy = inv.ModifiedBy,
                              ModifiedDate = inv.ModifiedDate,
                              ShowModifiedBy = user.FirstName + " " + user.LastName,

                          };

                if (invs.FirstOrDefault() != null)
                    return invs.FirstOrDefault();
                else
                    return new InvoiceAttachmentModel();
            }
        }

        public static RefundProcessModel GetRefundProcess(string clientCode)
        {
            using (var db = new QuickipediaEntities())
            {
                var process = from r in db.RefundProcess
                              join c in db.ClientProfile on r.ClientCode equals c.ClientCode
                              join u in db.UserAccount on r.ModifiedBy equals u.ID into qU
                              from user in qU.DefaultIfEmpty()
                              where r.ClientCode == clientCode
                              select new RefundProcessModel
                              {
                                  ID = r.ID,
                                  ClientCode = r.ClientCode,
                                  ClientName = c.ClientName,
                                  Process = r.RefundProcess1,
                                  ModifiedDate = r.ModifiedDate,
                                  ModifiedBy = r.ModifiedBy,
                                  ShowModifiedBy = user.FirstName + " " + user.LastName
                              };

                if (process.FirstOrDefault() != null)
                    return process.FirstOrDefault();
                else
                    return new RefundProcessModel();
            }
        }
        
        public static List<BillingCollectionFinanceModel> GetBillingCollectionFinance(string clientCode)
        {
            using (var db = new QuickipediaEntities())
            {
                var billing = from b in db.BillingCollectionFinanceManager
                              join c in db.ClientProfile on b.ClientCode equals c.ClientCode
                              join u in db.UserAccount on b.ModifiedBy equals u.ID into qU
                              from user in qU.DefaultIfEmpty()
                              where b.ClientCode == clientCode
                              select new BillingCollectionFinanceModel
                              {
                                  ID = b.ID,
                                  ClientCode = b.ClientCode,
                                  ClientName = c.ClientName,
                                  Name = b.Name,
                                  ContactNo = b.ContactNo,
                                  Email = b.Email,
                                  Position = b.Position,
                                  Status = "Y",
                                  ModifiedBy = b.ModifiedBy,
                                  ModifiedDate = b.ModifiedDate,
                                  ShowModifiedBy = user.FirstName + " " + user.LastName
                              };

                var billingList = billing.ToList();

                if (billingList != null)
                {
                   for(int ctr = 0; ctr < billing.ToList().Count; ctr++)
                    {
                        billingList[ctr].IDNo = ctr + 1;
                    }

                    return billingList;
                }
                else
                    return new List<BillingCollectionFinanceModel>();
            }
        }

        public static FormOfPaymentModel GetFormOfPayment(string clientCode)
        {
            using (var db = new QuickipediaEntities())
            {
                var form = from f in db.FormOfPayment
                           join c in db.ClientProfile on f.ClientCode equals c.ClientCode
                           join u in db.UserAccount on f.ModifiedBy equals u.ID into qU
                           from user in qU.DefaultIfEmpty()
                           where f.ClientCode == clientCode
                           select new FormOfPaymentModel
                           {
                               ID = f.ID,
                               ClientCode = f.ClientCode,
                               ClientName = c.ClientName,
                               FOP = f.FOP,
                               CCLiquidationProcess = f.CCLiquidationProcess,
                               TaxType = f.TaxType,
                               IssuedCard = f.IssuedCard,
                               ModifiedDate = f.ModifiedDate,
                               ModifiedBy = f.ModifiedBy,
                               ShowModifiedBy = user.FirstName + " " + user.LastName,
                               FOP_Others = f.FOP_Others,
                               CCLiquidation_Others = f.CCLiquidation_Others,
                               IssuedCard_Others = f.IssuedCard_Others,
                               MerchantFee = f.MerchantFee,
                               Currency = f.Currency,
                               OtherCurrency = f.Other_Currency
                           };

                if (form.FirstOrDefault() != null)
                    return form.FirstOrDefault();
                else
                    return new FormOfPaymentModel();
            }
        }

        public static PricingModels GetPricingModel(string clientCode)
        {
            using (var db = new QuickipediaEntities())
            {
                var pricingModel = from p in db.PricingModel
                                   join c in db.ClientProfile on p.ClientCode equals c.ClientCode
                                   join u in db.UserAccount on p.ModifiedBy equals u.ID into qU
                                   from user in qU.DefaultIfEmpty()
                                   where p.ClientCode == clientCode
                                   select new PricingModels
                                   {
                                       ID = p.ID,
                                       BundleFlag = p.BundleFlag,
                                       ClientCode = p.ClientCode,
                                       ClientName = c.ClientName,
                                       RetainFlag = p.RetainFlag,
                                       ReturnFlag = p.ReturnFlag,
                                       UnbundledFlag = p.UnbundledFlag,
                                       ModifiedBy = p.ModifiedBy,
                                       ModifiedDate = p.ModifiedDate,
                                       ShowModifiedBy = user.FirstName + " " + user.LastName
                                   };

                if (pricingModel.FirstOrDefault() == null)
                    return new PricingModels();
                else
                    return pricingModel.FirstOrDefault();
            }
        }

        public static ScheduleOfInvoiceModel GetScheduleOfInvoice(out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var query = from s in db.ScheduleOfInvoiceReceiving
                                join u in db.UserAccount on s.ModifiedBy equals u.ID into qU
                                from user in qU.DefaultIfEmpty()
                                where s.ClientCode == UniversalHelpers.SelectedClient
                                select new ScheduleOfInvoiceModel
                                {
                                    ID = s.ID,
                                    Value = s.Value,
                                    ClientCode = s.ClientCode,
                                    ModifiedDate = s.ModifiedDate,
                                    ModifiedBy = s.ModifiedBy,
                                    ShowModifiedBy = user.FirstName + " " + user.LastName
                                };

                    if (query.FirstOrDefault() != null)
                        return query.FirstOrDefault();
                    else
                        return new ScheduleOfInvoiceModel();
                }
            }
            catch (Exception error)
            {
                message = error.Message;

                return new ScheduleOfInvoiceModel();
            }
        }

        public static void SaveScheduleOfInvoice(ScheduleOfInvoiceModel model, out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var sched = db.ScheduleOfInvoiceReceiving.FirstOrDefault(r => r.ClientCode == UniversalHelpers.SelectedClient);

                    if (sched != null)//UPDATE
                    {
                        message = "Updated";

                        sched.Value = model.Value;

                        sched.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                        sched.ModifiedDate = DateTime.Now;

                        db.Entry(sched).State = EntityState.Modified;
                    }
                    else //SAVE
                    {
                        message = "Saved";

                        ScheduleOfInvoiceReceiving newSched = new ScheduleOfInvoiceReceiving
                        {
                            ID = Guid.NewGuid(),
                            ClientCode = UniversalHelpers.SelectedClient,
                            Value = model.Value,
                            ModifiedDate = DateTime.Now,
                            ModifiedBy = UniversalHelpers.CurrentUser.ID
                        };

                        db.Entry(newSched).State = EntityState.Added;
                    }

                    db.SaveChanges();
                }
            }
            catch (Exception error)
            {
                message = error.Message;
            }
        }
    }
}