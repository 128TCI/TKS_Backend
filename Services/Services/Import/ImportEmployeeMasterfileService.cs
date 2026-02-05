using ClosedXML.Excel;
using DomainEntities.Dto;
using DomainEntities.DTO;
using Infrastructure.IRepositories.Import;
using Infrastructure.IRepositories.LeaveTypes;
using Infrastructure.IRepositories.Maintenance;
using Microsoft.AspNetCore.Http;
using Services.DTOs.Encryption;
using Services.Interfaces.Import;
using Services.Interfaces.Maintenence;

namespace Services.Services.Import
{
    public class ImportEmployeeMasterfileService(
        IImportEmployeeMasterfileRepository repository,
        EncryptionHelper crypto,
        IEmployeeMasterFileService masterFileService,

        IEmployeeMasterFileRepository employeeMasterFileRepository) : IImportEmployeeMasterfileService
    {
        private readonly IImportEmployeeMasterfileRepository _repository = repository;
        private readonly IEmployeeMasterFileService _masterFileService = masterFileService;
        private readonly EncryptionHelper _crypto = crypto;
        private readonly IEmployeeMasterFileRepository _employeeMasterFileRepository = employeeMasterFileRepository;

        public async Task<ReturnResult<List<ImportEmployeeMasterfileDto>>> ImportEmployeeMasterfile(IFormCollection form, CancellationToken ct)
        {
            if (form.Files.Count == 0)
                return new ReturnResult<List<ImportEmployeeMasterfileDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No File found."]);

            using MemoryStream stream = new();

            await form.Files[0].CopyToAsync(stream, ct);
            stream.Position = 0;

            using XLWorkbook workbook = new(stream);
            IXLWorksheet worksheet = workbook.Worksheet(1);

            int lastRow = worksheet.LastRowUsed()?.RowNumber() ?? 0;
            IXLCell? lastCell = worksheet.Row(2).CellsUsed()
                      .Where(c => c.Address.ColumnNumber >= 4)
                      .LastOrDefault();
            int lastColumn = lastCell?.Address.ColumnNumber ?? 0;
            if (lastRow is 0)
                return new ReturnResult<List<ImportEmployeeMasterfileDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No Data in the file"]);
            if (lastColumn is 0)
                return new ReturnResult<List<ImportEmployeeMasterfileDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No Data in the file"]);

            List<ImportEmployeeMasterfileDto> imports = ImportEmpMasterFile(worksheet, lastRow, lastColumn);
            #region Validate Import Data
            ReturnResult<List<ImportEmployeeMasterfileDto>> errors = await ValidateImportEmployeeMasterfile(imports, ct);
            if (!errors.IsSuccess)
            {
                return errors;
            }
            #endregion
            return new ReturnResult<List<ImportEmployeeMasterfileDto>>(resultData: imports, messages: "");
        }

        public async Task<ReturnResult<string>> UpdateImportEmployeeMasterfile(List<ImportEmployeeMasterfileDto> param, CancellationToken ct)
        {
            var ek = _crypto.GetKey();
            #region Validate Import Data
            ReturnResult<List<ImportEmployeeMasterfileDto>> errors = await ValidateImportEmployeeMasterfile(param, ct);
            if (!errors.IsSuccess)
            {
                return new ReturnResult<string>(resultData: "", messages: ResponseMessage.ERROR, isSuccess: false, errors: ["Cannot proceed to import. Please fix the data in the import list."]);
            }
            #endregion
            foreach (var item in param)
            {
                item.LName = _crypto.Encrypt(item.LName!, ek);
                item.FName = _crypto.Encrypt(item.FName!, ek);
                item.MName = _crypto.Encrypt(item.MName!, ek);
                item.HAddress = _crypto.Encrypt(item.HAddress!, ek);
                item.PAddress = _crypto.Encrypt(item.PAddress!, ek);
                item.City = _crypto.Encrypt(item.City!, ek);
                item.Province = _crypto.Encrypt(item.Province!, ek);
                item.PostalCode = _crypto.Encrypt(item.PostalCode!, ek);
                item.CivilStatus = _crypto.Encrypt(item.CivilStatus!, ek);
                item.Citizenship = _crypto.Encrypt(item.Citizenship!, ek);
                item.MobilePhone = _crypto.Encrypt(item.MobilePhone!, ek);
                item.HomePhone = _crypto.Encrypt(item.HomePhone!, ek);
                item.PresentPhone = _crypto.Encrypt(item.PresentPhone!, ek);
                item.BirthPlace = _crypto.Encrypt(item.BirthPlace!, ek);
                item.SSSNo = _crypto.Encrypt(item.SSSNo!, ek);
                item.PhilHealthNo = _crypto.Encrypt(item.PhilHealthNo!, ek);
                item.TIN = _crypto.Encrypt(item.TIN!, ek);
                item.GSISNo = _crypto.Encrypt(item.GSISNo!, ek);
                item.Suffix = _crypto.Encrypt(item.Suffix!, ek);

                await _repository.UpdateImportEmployeeMasterfile(item);
            }
            return new ReturnResult<string>(resultData: "", messages: ResponseMessage.SAVE);
        }
        internal static List<ImportEmployeeMasterfileDto> ImportEmpMasterFile(IXLWorksheet worksheet, int lastRow, int lastColumn)
        {
            List<ImportEmployeeMasterfileDto> imports = [];
            int firstDataColumn = 1;
            
            for (int row = 2; row <= lastRow; row++)
            {  
                if (worksheet.Cell(row, 1).GetString() == null) continue;

                string empCode = worksheet.Cell(row, 1).Value.ToString();
                string statusActive = worksheet.Cell(row, 2).Value.ToString();
                string empStatCode = worksheet.Cell(row, 3).Value.ToString();
                string courtesy = worksheet.Cell(row, 4).Value.ToString();
                string lName = worksheet.Cell(row, 5).Value.ToString();
                string fName = worksheet.Cell(row, 6).Value.ToString();
                string mName = worksheet.Cell(row, 7).Value.ToString();
                string nickName = worksheet.Cell(row, 8).Value.ToString();
                string hAddress = worksheet.Cell(row, 9).Value.ToString();
                string pAddress = worksheet.Cell(row, 10).Value.ToString();
                string city = worksheet.Cell(row, 11).Value.ToString();
                string province = worksheet.Cell(row, 12).Value.ToString();
                string postalCode = worksheet.Cell(row, 13).Value.ToString();
                string civilStatus = worksheet.Cell(row, 14).Value.ToString();
                string citizenship = worksheet.Cell(row, 15).Value.ToString();
                string religion = worksheet.Cell(row, 16).Value.ToString();
                string sex = worksheet.Cell(row, 17).Value.ToString();
                string email = worksheet.Cell(row, 18).Value.ToString();
                string weight = worksheet.Cell(row, 19).Value.ToString();
                string height = worksheet.Cell(row, 20).Value.ToString();
                string mobilePhone = worksheet.Cell(row, 21).Value.ToString();
                string homePhone = worksheet.Cell(row, 22).Value.ToString();
                string presentPhone = worksheet.Cell(row, 23).Value.ToString();
                string birthPlace = worksheet.Cell(row, 24).Value.ToString();
                string braCode = worksheet.Cell(row, 25).Value.ToString();
                string divCode = worksheet.Cell(row, 26).Value.ToString();
                string depCode = worksheet.Cell(row, 27).Value.ToString();
                string secCode = worksheet.Cell(row, 28).Value.ToString();
                string unitCode = worksheet.Cell(row, 29).Value.ToString();
                string lineCode = worksheet.Cell(row, 30).Value.ToString();
                string desCode = worksheet.Cell(row, 31).Value.ToString();
                string superior = worksheet.Cell(row, 32).Value.ToString();
                string grdCode = worksheet.Cell(row, 33).Value.ToString();
                string sssNo = worksheet.Cell(row, 34).Value.ToString();
                string philHealthNo = worksheet.Cell(row, 35).Value.ToString();
                string tin = worksheet.Cell(row, 36).Value.ToString();

                //datetime
                string dHired = worksheet.Cell(row, 37).Value.ToString();
                string dRegularized = worksheet.Cell(row, 38).Value.ToString();
                string dResigned = worksheet.Cell(row, 39).Value.ToString();
                string dSuspended = worksheet.Cell(row, 40).Value.ToString();
                string pStart = worksheet.Cell(row, 41).Value.ToString();
                string pEnd = worksheet.Cell(row, 42).Value.ToString();
                string bDate = worksheet.Cell(row, 43).Value.ToString();

                DateTime dateHired = Convert.ToDateTime(dHired);
                DateTime dateRegularized = Convert.ToDateTime(dRegularized);
                DateTime dateResigned = Convert.ToDateTime(dResigned);
                DateTime dateSuspended = Convert.ToDateTime(dSuspended);
                DateTime probeStart = Convert.ToDateTime(pStart);
                DateTime probeEnd = Convert.ToDateTime(pEnd);
                DateTime birthDate = Convert.ToDateTime(bDate);

                //string
                string tksGroup = worksheet.Cell(row, 44).Value.ToString();
                string groupSchedCode = worksheet.Cell(row, 45).Value.ToString();
                string allowOTDefault = worksheet.Cell(row, 46).Value.ToString();
                string tardyExemp = worksheet.Cell(row, 47).Value.ToString();
                string utExempt = worksheet.Cell(row, 48).Value.ToString();
                string ndExempt = worksheet.Cell(row, 49).Value.ToString();
                string otExempt = worksheet.Cell(row, 50).Value.ToString();
                string absenceExempt = worksheet.Cell(row, 51).Value.ToString();
                string otherEarnExempt = worksheet.Cell(row, 52).Value.ToString();
                string holidayExempt = worksheet.Cell(row, 53).Value.ToString();
                string unproductiveExempt = worksheet.Cell(row, 54).Value.ToString();
                string deviceCode = worksheet.Cell(row, 55).Value.ToString();
                string fixedRestDay1 = worksheet.Cell(row, 56).Value.ToString();
                string fixedRestDay2 = worksheet.Cell(row, 57).Value.ToString();
                string fixedRestDay3 = worksheet.Cell(row, 58).Value.ToString();
                string dailySchedule = worksheet.Cell(row, 59).Value.ToString();
                string classificationCode = worksheet.Cell(row, 60).Value.ToString();
                string gsisNo = worksheet.Cell(row, 61).Value.ToString();
                string suffix = worksheet.Cell(row, 62).Value.ToString();
                string onlineAppCode = worksheet.Cell(row, 63).Value.ToString();


                if (string.IsNullOrEmpty(empCode))
                    continue;

                for (int col = firstDataColumn; col <= lastColumn; col++)
                {
                    imports.Add(new ImportEmployeeMasterfileDto
                    {
                        EmpCode = empCode,
                        StatusActive = statusActive,
                        EmpStatCode = empStatCode,
                        Courtesy = courtesy,
                        LName = lName,
                        FName = fName,
                        MName = mName,
                        NickName = nickName,
                        HAddress = hAddress,
                        PAddress = pAddress,
                        City = city,
                        Province = province,
                        PostalCode = postalCode,
                        CivilStatus = civilStatus,
                        Citizenship = citizenship,
                        Religion = religion,
                        Sex = sex,
                        Email = email,
                        Weight = weight,
                        Height = height,
                        MobilePhone = mobilePhone,
                        HomePhone = homePhone,
                        PresentPhone = presentPhone,
                        BirthPlace = birthPlace,
                        BraCode = braCode,
                        DivCode = divCode,
                        DepCode = depCode,
                        SecCode = secCode,
                        UnitCode = unitCode,
                        LineCode = lineCode,
                        DesCode = desCode,
                        Superior = superior,
                        GrdCode = grdCode,
                        SSSNo = sssNo,
                        PhilHealthNo = philHealthNo,
                        TIN = tin,
                        DateHired = dateHired,
                        DateRegularized = dateRegularized,
                        DateSuspended = dateSuspended,
                        ProbeStart = probeStart,
                        ProbeEnd = probeEnd,
                        BirthDate = birthDate,
                        TKSGroup = tksGroup,
                        GroupSchedCode = groupSchedCode,
                        AllowOTDefault = allowOTDefault,
                        TardyExemp = tardyExemp,
                        UTExempt = utExempt,
                        NDExempt = ndExempt,
                        OTExempt = otExempt,
                        AbsenceExempt = absenceExempt,
                        OtherEarnExempt = otherEarnExempt,
                        HolidayExempt = holidayExempt,
                        UnproductiveExempt = unproductiveExempt,
                        DeviceCode = deviceCode,
                        FixedRestDay1 = fixedRestDay1,
                        FixedRestDay2 = fixedRestDay2,
                        FixedRestDay3 = fixedRestDay3,
                        DailySchedule = dailySchedule,
                        ClassificationCode = classificationCode,
                        GSISNo = gsisNo,
                        Suffix = suffix,
                        OnlineAppCode = onlineAppCode
                    });
                }
            }

            return [.. imports];
        }
        internal async Task<ReturnResult<List<ImportEmployeeMasterfileDto>>> ValidateImportEmployeeMasterfile(
                   List<ImportEmployeeMasterfileDto> imports, CancellationToken ct)
        {
            if (imports.Count == 0)
            {
                return new ReturnResult<List<ImportEmployeeMasterfileDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No Data"]);
            }
            
            List<ImportEmployeeMasterfileDto> errors = [];
            for (int i = 0; i < imports.Count; i++)
            {
                ImportEmployeeMasterfileDto import = imports[i];
                string message = "";
                var masterFile = await _masterFileService.GetAllAsync();

                if (masterFile.Where(i => i.SSSNo == import.SSSNo && i.EmpCode != import.EmpCode).Any())
                {
                    message += "SSS Number already exists with other EmpCode.";
                }

                if (masterFile.Where(i => i.TIN == import.TIN && i.EmpCode != import.EmpCode).Any())
                {
                    message += "TIN Number already exists with other EmpCode.";
                }

                if (masterFile.Where(i => i.PHilHealthNo == import.PhilHealthNo && i.EmpCode != import.EmpCode).Any())
                {
                    message += "PhilHealth Number already exists with other EmpCode.";
                }

                if (masterFile.Where(i => i.GSISNo == import.GSISNo && i.EmpCode != import.EmpCode).Any())
                {
                    message += "GSIS Number already exists with other EmpCode.";
                }

                if (!string.IsNullOrEmpty(message))
                {
                    message += $". Row Number {import.RowNumber}, Column {import.ColumnNumber}";
                    if (!errors.Where(x => x.EmpCode == import.EmpCode).Any())
                    {
                        errors.Add(new ImportEmployeeMasterfileDto
                        {
                            EmpCode = import.EmpCode,
                            StatusActive = import.StatusActive,
                            EmpStatCode = import.EmpStatCode,
                            Courtesy = import.Courtesy,
                            LName = import.LName,
                            FName = import.FName,
                            MName = import.MName,
                            NickName = import.NickName,
                            HAddress = import.HAddress,
                            PAddress = import.PAddress,
                            City = import.City,
                            Province = import.Province,
                            PostalCode = import.PostalCode,
                            CivilStatus = import.CivilStatus,
                            Citizenship = import.Citizenship,
                            Religion = import.Religion,
                            Sex = import.Sex,
                            Email = import.Email,
                            Weight = import.Weight,
                            Height = import.Height,
                            MobilePhone = import.MobilePhone,
                            HomePhone = import.HomePhone,
                            PresentPhone = import.PresentPhone,
                            BirthPlace = import.BirthPlace,
                            BraCode = import.BraCode,
                            DivCode = import.DivCode,
                            DepCode = import.DepCode,
                            SecCode = import.SecCode,
                            UnitCode = import.UnitCode,
                            LineCode = import.LineCode,
                            DesCode = import.DesCode,
                            Superior = import.Superior,
                            GrdCode = import.GrdCode,
                            SSSNo = import.SSSNo,
                            PhilHealthNo = import.PhilHealthNo,
                            TIN = import.TIN,
                            DateHired = import.DateHired,
                            DateRegularized = import.DateRegularized,
                            DateSuspended = import.DateSuspended,
                            ProbeStart = import.ProbeStart,
                            ProbeEnd = import.ProbeEnd,
                            BirthDate = import.BirthDate,
                            TKSGroup = import.TKSGroup,
                            GroupSchedCode = import.GroupSchedCode,
                            AllowOTDefault = import.AllowOTDefault,
                            TardyExemp = import.TardyExemp,
                            UTExempt = import.UTExempt,
                            NDExempt = import.NDExempt,
                            OTExempt = import.OTExempt,
                            AbsenceExempt = import.AbsenceExempt,
                            OtherEarnExempt = import.OtherEarnExempt,
                            HolidayExempt = import.HolidayExempt,
                            UnproductiveExempt = import.UnproductiveExempt,
                            DeviceCode = import.DeviceCode,
                            FixedRestDay1 = import.FixedRestDay1,
                            FixedRestDay2 = import.FixedRestDay2,
                            FixedRestDay3 = import.FixedRestDay3,
                            DailySchedule = import.DailySchedule,
                            ClassificationCode = import.ClassificationCode,
                            GSISNo = import.GSISNo,
                            Suffix = import.Suffix,
                            OnlineAppCode = import.OnlineAppCode,
                            Message = message,
                            RowNumber = import.RowNumber,
                            ColumnNumber = import.ColumnNumber
                        });
                    }
                }
            }
            return new ReturnResult<List<ImportEmployeeMasterfileDto>>(resultData: errors, messages: errors.Count is 0 ? "" : "Error, Please see message in the table below", isSuccess: errors.Count is 0, errors: ["Error, Please see message in the table below"]);
        }
    }
}
