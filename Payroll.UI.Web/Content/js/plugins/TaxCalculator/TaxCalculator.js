// Formats numeric values (float, integer or string) to currency symbol followed 
// by the number with thousands delimiter separated and to two decimal places (rounded to nearest)
// eg: FormatMoney('12345678.9', 'R ', ',');  returns 'R 12,345,678.90'
// Matthew 18/1/2000
function FormatMoney(num, currencySymbol, thousandsDelimiter) {
    var numStr = '' + Math.round(((typeof (num) == 'string') ? parseFloat(num, 10) : num) * 100) / 100;
    thousandsDelimiter = (typeof (thousandsDelimiter) == 'undefined') ? '' : thousandsDelimiter;
    currencySymbol = (typeof (currencySymbol) == 'undefined') ? '' : currencySymbol;
    if (numStr.length > 2) {
        if (numStr.length - numStr.indexOf('.') == 2)
            numStr += '0';
        if (numStr.length - numStr.indexOf('.') != 3)
            numStr += '.00';
    }
    else
        numStr += '.00';
    if (numStr.indexOf('.') > 3 && thousandsDelimiter != '') {
        if (numStr.substring(0, numStr.indexOf('.') - 3) != '-')
            numStr = numStr.substring(0, numStr.indexOf('.') - 3) + thousandsDelimiter + numStr.substring(numStr.indexOf('.') - 3)
        var pos = 7;
        while (numStr.indexOf(thousandsDelimiter) > 3) {
            if (numStr.substring(0, numStr.indexOf('.') - pos) != '-')
                numStr = numStr.substring(0, numStr.indexOf('.') - pos) + thousandsDelimiter + numStr.substring(numStr.indexOf('.') - pos)
            else
                return currencySymbol + numStr;
            pos += 4;
        }
    }
    return currencySymbol + numStr;
}

function removeSpaces(string) {
    var tstring = "";
    string = '' + string;
    splitstring = string.split(" ");
    for (i = 0; i < splitstring.length; i++)
        tstring += splitstring[i];
    return tstring;
}

$(document).ready(function () {


    $('#my10cents').remove();

    var objTaxCalculatorMy10cents = _my10cents[0];

    $('h3.animate').click(function () {
        $(this).next('div').toggle();
        $(this).find('span').toggleClass('open');
    });

    function TaxBand(marginalRate, baseAmount, threshold, limit) {
        this.marginalRate = marginalRate;
        this.baseAmount = baseAmount;
        this.threshold = threshold;
        this.limit = (arguments.length > 3) ? this.limit = limit : this.limit = Number.POSITIVE_INFINITY;
    }
    /*
    //TAX TABLES 2015/16 --------------------------------------------------
    //current year
    var TaxTableNew = new Array(
		new TaxBand(0.18, 0, 0, 181900),
		new TaxBand(0.26, 32742, 181901, 284100),
		new TaxBand(0.31, 59314, 284101, 393200),
		new TaxBand(0.36, 93135, 393201, 550100),
		new TaxBand(0.39, 149619, 550101, 701300),
		new TaxBand(0.41, 208587, 701301));
    //previous year
    var TaxTableOld = new Array(
		new TaxBand(0.18, 0, 0, 174550),
		new TaxBand(0.25, 31419, 174551, 272700),
		new TaxBand(0.30, 55957, 272701, 377450),
		new TaxBand(0.35, 87382, 377451, 528000),
		new TaxBand(0.38, 140074, 528001, 673100),
		new TaxBand(0.40, 195212, 673101));
    */

    //TAX TABLES 2016/17 --------------------------------------------------
    //current year
    var TaxTableNew = new Array(
		new TaxBand(0.18, 0, 0, 188000),
		new TaxBand(0.26, 33840, 188001, 293600),
		new TaxBand(0.31, 61296, 293601, 406400),
		new TaxBand(0.36, 96264, 406401, 550100),
		new TaxBand(0.39, 147996, 550101, 701300),
		new TaxBand(0.41, 206964, 701301));
    //previous year
    var TaxTableOld = new Array(
		new TaxBand(0.18, 0, 0, 181900),
		new TaxBand(0.26, 32742, 181901, 284100),
		new TaxBand(0.31, 59314, 284101, 393200),
		new TaxBand(0.36, 93135, 393201, 550100),
		new TaxBand(0.39, 149619, 550101, 701300),
		new TaxBand(0.41, 208587, 701301));



    function CalculateIncomeTax(bandArray, annualTaxableIncome) {
        for (var i = 0; i < bandArray.length; i++) {
            if ((annualTaxableIncome >= bandArray[i].threshold) && (annualTaxableIncome <= bandArray[i].limit)) {
                return bandArray[i].baseAmount + (bandArray[i].marginalRate * (annualTaxableIncome - bandArray[i].threshold));
            }
        };
    }

    function CalculateRebate(age) {
        /* 2015
        var primary_rebate = 13257;     // < 65
        var secondary_rebate = 7407;    // >= 65
        var tertiary_rebate = 2466;     // >= 75
        */
        var primary_rebate = 13500;     // < 65
        var secondary_rebate = 7407;    // >= 65
        var tertiary_rebate = 2466;     // >= 75

        var rebate = primary_rebate;
        if ((age >= 65) && (age < 75)) { rebate = rebate + secondary_rebate; }
        if (age >= 75) { rebate = rebate + secondary_rebate + tertiary_rebate; }

        return rebate;
    }

    //inputs are annual
    function CalculateAnnualMedicalTaxCredit(numberofdependants, booldisabled, age, medicalaidcontributions, outofpocketexpenses, taxableincome) {

        if (medicalaidcontributions == 0) { return 0; }
        /*
        var _medical_tax_credit = 270;
        for (var i = 1; i <= numberofdependants; i++) {
            _medical_tax_credit = (i == 1) ? _medical_tax_credit + 270 : _medical_tax_credit + 181;
        }
        */
        var _medical_tax_credit = 286;
        for (var i = 1; i <= numberofdependants; i++) {
            _medical_tax_credit = (i == 1) ? _medical_tax_credit + 286 : _medical_tax_credit + 192;
        }
        _medical_tax_credit = _medical_tax_credit * 12;
        //console.log("_medical_tax_credit", _medical_tax_credit);
        //consider any out of pocket expenses
        var outofpocketexpenses_credit = 0;
        var medicalcredit_A = 0;
        var medicalcredit_B = 0;
        var medicalcredit_C = 0;
        var medicalcredit_D = 0;
        //if (outofpocketexpenses > 0) {
        //if >= 65 or disabled
        if ((booldisabled == true) || (age >= 65)) {
            $('#derived_disabled_flag').html('Disabled or 65 and over');
            medicalcredit_A = ((medicalaidcontributions - (3 * _medical_tax_credit)) > 0) ? (medicalaidcontributions - (3 * _medical_tax_credit)) : 0;
            medicalcredit_B = outofpocketexpenses;
            outofpocketexpenses_credit = (medicalcredit_A * (33.3 / 100)) + (medicalcredit_B * (33.3 / 100))
            //console.log("outofpocketexpenses_credit", outofpocketexpenses_credit);
        } else {
            //if < 65
            $('#derived_disabled_flag').html('under 65');
            medicalcredit_A = ((medicalaidcontributions - (4 * _medical_tax_credit)) > 0) ? (medicalaidcontributions - (4 * _medical_tax_credit)) : 0;
            medicalcredit_B = outofpocketexpenses;
            medicalcredit_C = medicalcredit_A + medicalcredit_B;
            medicalcredit_D = ((medicalcredit_C - (taxableincome * (7.5 / 100))) > 0) ? (medicalcredit_C - (taxableincome * (7.5 / 100))) : 0;
            outofpocketexpenses_credit = medicalcredit_D * (25 / 100);
        }
        //}
        $('#derived_medicalcredit_A').html(medicalcredit_A);
        $('#derived_medicalcredit_B').html(medicalcredit_B);
        $('#derived_medicalcredit_C').html(medicalcredit_C);
        $('#derived_medicalcredit_D').html(medicalcredit_D);

        return (_medical_tax_credit + outofpocketexpenses_credit);
    }

    $('#calculate').click(function () {
        if (($('#age').val() == '0' && $('#age').val() == '') || ($('#salary').val() == '0' || $('#salary').val() == '')) { alert('Please complete required input fields.'); return false; }
        $("#inputs input[type=text]").each(function () { if ($(this).val() === '') $(this).val(0); })
        if ($("#inputs").validation()) {
            //INPUTS-------------------------------------------------
            var age = $('#age').val();
            var monthly_salary = removeSpaces($('#salary').val());
            var otherincome = removeSpaces($('#otherincome').val());
            var pensionamount = removeSpaces($('#pensionamount').val());

            //var pensionpercentage_employee = removeSpaces($('#pensionpercentage_employee').val());            
            //var pensionamount_employee = (pensionpercentage_employee > 0) ? (monthly_salary * pensionpercentage_employee) / 100 : 0; FOR 2016

            var raamount = removeSpaces($('#raamount').val());
            var medicalaidcontributions = removeSpaces($('#medicalaidcontributions').val());
            var medicaloutofpocket = removeSpaces($('#medicaloutofpocket').val());
            var annual_salary = parseFloat(monthly_salary) * 12;
            var annual_otherincome = parseFloat(otherincome) * 12;
            //var annual_pensionamount = parseFloat(pensionamount) * 12;
            var annual_pensionamount = pensionamount * 12;  //(pensionamount_employer + pensionamount_employee) * 12; FOR 2016
            var annual_raamount = parseFloat(raamount) * 12;
            var medicalaid_contributions = parseFloat(medicalaidcontributions) * 12;
            var medical_outof_pocket = parseFloat(medicaloutofpocket) * 12;
            var number_of_dependants = $('#numberofdependants').val();
            var disabled_dependants = $('#disableddependants').is(':checked');
            // Employer contribution to risk benefits
            var riskbenefitpercentage = $('#riskbenefitpercentage').val();
            var riskbenefitamount = (annual_salary * riskbenefitpercentage) / 100;//monthly_salary * (riskbenefitpercentage / 100);
            // Employer contribution to retirement funding
            var pensionpercentage_employer = removeSpaces($('#pensionpercentage_employer').val());
            var pensionamount_employer = (pensionpercentage_employer > 0) ? (annual_salary * pensionpercentage_employer) / 100 : 0;
            // Fringe benefits
            var fringebenefits = riskbenefitamount + pensionamount_employer;
            var annual_fringebenefits = fringebenefits * 12;


            //console.log("riskbenefitamount " + riskbenefitamount);
            //DERIVED------------------------------------------------
            //pension 2015
            //var max_pensionamount = Math.max((annual_salary + annual_fringebenefits) * (7.5 / 100), 1750); //TRISTAN - is this right?

            //pension 2016
            // Remuneration amount
            var remuneration = annual_salary + pensionamount_employer + riskbenefitamount;
            // Taxable income
            var taxable_income = remuneration + annual_otherincome;
            // Calculate the maximum retirement fund deduction for 2016
            var max_pensionamount = Math.min(350000, Math.round(0.275 * Math.max(remuneration, taxable_income)));

            //var max_taxable_income = (annual_salary + annual_otherincome + fringebenefits) - (max_pensionamount + 20000);

            derived_annual_pensionamount = (annual_pensionamount > max_pensionamount) ? max_pensionamount : annual_pensionamount;
            //show allowable pension amount
            $('#derived_pensionamount').html(derived_annual_pensionamount / 12);
            //console.log("max_pensionamount " + max_pensionamount);
            //console.log("derived_annual_pensionamount " + derived_annual_pensionamount);
            //ra
            var derived_annual_raamount = (derived_annual_pensionamount == 0) ? ((annual_salary + annual_otherincome) * (15 / 100)) : (annual_otherincome * (15 / 100));
            var max_annual_raamount = Math.max(derived_annual_raamount, 3500 - derived_annual_pensionamount, 1750);
            var actual_annual_raamount = (annual_raamount < max_annual_raamount) ? annual_raamount : max_annual_raamount;
            //show actual ra amount
            $('#derived_raamount').html(actual_annual_raamount / 12);

            // Retirement funds
            var retirementfunds = ((pensionamount_employer + annual_pensionamount + annual_raamount) > max_pensionamount) ? max_pensionamount : (pensionamount_employer + annual_pensionamount + annual_raamount);

            //derived taxable income
            //var derived_annual_taxable_income = (annual_salary + annual_otherincome + annual_fringebenefits) - (derived_annual_pensionamount + actual_annual_raamount);
            var derived_annual_taxable_income = (annual_salary + annual_otherincome + fringebenefits) - (retirementfunds);

            var derived_max_annual_taxable_income = (annual_salary + annual_otherincome + fringebenefits) - (max_pensionamount);

            //console.log(derived_annual_taxable_income);
            //show annual taxable income
            $('#derived_taxable_income').html(derived_annual_taxable_income / 12);
            //console.log("plus: " + (annual_salary + annual_otherincome + annual_fringebenefits));
            //console.log("minus: " + (derived_annual_pensionamount + actual_annual_raamount));
            //console.log("Taxable Income: " + derived_annual_taxable_income);


            //GROSS INCOME TAX---------------------------------------
            var gross_income_tax = CalculateIncomeTax(TaxTableNew, derived_annual_taxable_income);
            //PREVIOUS YEAR INCOME TAX
            var previous_gross_income_tax = CalculateIncomeTax(TaxTableOld, derived_annual_taxable_income);
            //MAX GROSS INCOME TAX
            var gross_max_income_tax = CalculateIncomeTax(TaxTableNew, derived_max_annual_taxable_income);

            //REBATES------------------------------------------------
            var income_tax_rebate = CalculateRebate(age);
            //show rebate
            $('#derived_rebate').html(income_tax_rebate);
            var gross_income_tax_less_rebate = gross_income_tax - income_tax_rebate;
            var previous_gross_income_tax_less_rebate = previous_gross_income_tax - income_tax_rebate;
            $('#derived_gross_income_tax_less_rebate').html(gross_income_tax_less_rebate / 12);

            //MEDICAL TAX CREDITS------------------------------------
            var medical_tax_credit = CalculateAnnualMedicalTaxCredit(number_of_dependants, disabled_dependants, age, medicalaid_contributions, medical_outof_pocket, derived_annual_taxable_income);
            //show medical credit
            $('#derived_medical_tax_credit').html(medical_tax_credit);
            //MAX MEDICAL TAX CREDITS
            var max_medical_tax_credit = CalculateAnnualMedicalTaxCredit(number_of_dependants, disabled_dependants, age, medicalaid_contributions, medical_outof_pocket, derived_max_annual_taxable_income);
            //console.log("medical tax credit: " + medical_tax_credit);

            var gross_income_tax_monthly = ((gross_income_tax_less_rebate - medical_tax_credit) / 12);
            var previous_income_tax_monthly = ((previous_gross_income_tax_less_rebate) / 12)

            var monthly_net_income = (parseFloat(monthly_salary) + parseFloat(otherincome)) - ((gross_income_tax_less_rebate - medical_tax_credit) / 12);

            // The same as tax due after applying rebates and credits (monthly)
            var income_tax = (gross_income_tax_less_rebate - medical_tax_credit) / 12;

            // Tax due after applying the rebates and credits (annually)
            var annual_tax_due = (gross_income_tax_less_rebate - medical_tax_credit);

            //Tax due after applying the rebates and credits (monthly)
            var monthly_tax_due = (gross_income_tax_less_rebate - medical_tax_credit) / 12;

            // Amount still allowed as a deduction
            var allowed_deduction = ((retirementfunds < max_pensionamount) ? (max_pensionamount - retirementfunds) : 0) / 12;

            // Tax due after applying additional contribution (annually) - Sheet2!D25 
            var annual_tax_after_additional_contribution = gross_max_income_tax - (income_tax_rebate + max_medical_tax_credit);

            // Tax due after applying additional contribution (monthly)
            var monthly_tax_after_additional_contribution = (annual_tax_after_additional_contribution / 12);

            // Savings after applying additional contribution (annually)
            var annual_savings_with_additonal_contribution = annual_tax_due - annual_tax_after_additional_contribution;

            // Savings after applying additional contribution (monthly)
            var monthly_savings_with_additonal_contribution = (annual_savings_with_additonal_contribution / 12);

            // Monthly take home (After contributing max)
            var month_cash_after_max_contribution = (parseFloat(monthly_salary) + parseFloat(otherincome)) - monthly_tax_after_additional_contribution;


            //OUTPUT-------------------------------------------------
            if (income_tax > 0) {
                $('#gross_income_tax').html(FormatMoney(income_tax, 'R ', ','));
            }
            else {
                $('#gross_income_tax').html(FormatMoney(0, 'R ', ','));
            }
            $('#net_monthly_income').html(FormatMoney(monthly_net_income, 'R ', ','));
            $('#diff_monthly_tax').html(FormatMoney((gross_income_tax_monthly - previous_income_tax_monthly), 'R ', ','));
            // Newly added output fields
            $('#deduction_allowed').html(FormatMoney(allowed_deduction, 'R', ','));
            //$('#annual_tax_after_additional_contribution').html(FormatMoney(annual_tax_after_additional_contribution, 'R', ','));
            $('#monthly_tax_after_additional_contribution').html(FormatMoney(monthly_tax_after_additional_contribution, 'R', ','));
            //$('#annual_savings_with_additonal_contribution').html(FormatMoney(annual_savings_with_additonal_contribution, 'R', ','));
            $('#monthly_savings_with_additonal_contribution').html(FormatMoney(monthly_savings_with_additonal_contribution, 'R', ','));
            //$('#month_cash_after_max_contribution').html(FormatMoney(month_cash_after_max_contribution, 'R', ','));


            //SHOWS OUTPUT ON THE PAGE-----------------------------
            $('#outputs').slideDown('fast');
            //$('#my10cents').slideDown('fast');
            $('#calculate').find('span').html('Recalculate');
            $("#inputs input[type=text]").each(function () { if ($(this).val() == '0') $(this).val(''); })
        }
    });

    $('#btnSubmit').click(function () {
        var $form = $('#my10cents .wrapper');

        if ($form.validation()) {
            if (!$('#cbTerms').is(':checked')) { alert('Please accept the terms and conditions'); return false; }

            objTaxCalculatorMy10cents.FirstName = $('#txtFirstName').val();
            objTaxCalculatorMy10cents.Surname = $('#txtLastName').val();
            objTaxCalculatorMy10cents.Email = $('#txtEmailAddress').val();
            objTaxCalculatorMy10cents.My10centsComment = $('#txtMy10cents').val();
            objTaxCalculatorMy10cents.Marketing = $('#cbTerms').is(':checked');

            objTaxCalculatorMy10cents.Age = $('#age').val();
            objTaxCalculatorMy10cents.MonthlySalary = $('#salary').val();
            objTaxCalculatorMy10cents.OtherIncome = $('#otherincome').val();
            objTaxCalculatorMy10cents.PensionFundContribution = $('#pensionamount').val();
            objTaxCalculatorMy10cents.RAContribution = $('#raamount').val();
            objTaxCalculatorMy10cents.MedicalAidContribution = $('#medicalaidcontributions').val();
            objTaxCalculatorMy10cents.OutOfPocketExpenses = $('#medicaloutofpocket').val();
            objTaxCalculatorMy10cents.NumberDependants = $('#numberofdependants').val();
            objTaxCalculatorMy10cents.Disability = $('#disableddependants').is(':checked');

            AjaxMethodJSON("/v5/webservices/Forms.asmx/InsertTaxCalculatorMy10cents", objTaxCalculatorMy10cents, SubmitResults, AjaxFailed);

        }
    });


    function SubmitResults(result) {
        if (result !== null) {
            var obj = result.d;
            var objTaxCalculatorMy10cents = JSON.parse(obj);

            window.location.href = 'http://www.oldmutual.co.za/markets/south-african-budget.aspx#b_competition';
        }


    }

});