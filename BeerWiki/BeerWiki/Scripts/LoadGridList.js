var id = "";
var name = "";
var styleId = "";
var status = "";
var isOrganic = "";
var filterCount = 0;

$(document).ready(function () {
    var windowWidth = $(window).width();
    $(".body-content").css("margin-left", (windowWidth - 1330) / 2);
    LoadAllBeerData();
});

function StartValidation() {
    if ($("#searchText").val() == "") {
        alert('Please enter text');
        return false;
    }
    return true;
}

$('#btnReset').click(function () {
    //LoadAllBeerData();
    $.ajax({
        url: "GetAllBeerList",
        type: "Get",
        data: { sort: "", page: 1, rows: 50, searchString:"" },
        cache: false,
        async: true,
        success: LoadBeerData,
        OnFailure: OnFailure
    });
    $("#searchText").val("");
});


function LoadAllBeerData() {
    $("#BeerGridList").jqGrid
        ({
            url: 'GetAllBeerList',
            datatype: 'json',
            mtype: 'Get',
            //table header name
            colNames: ['BeerId', 'Beer Name', 'Beer Description',
                'Alcohol Volumn', 'International Bittering Unit', 'Style ID', 'Status', 'Created Date', 'Updated Date', 'Organic', 'Category Name'],
            //colModel takes the data from controller and binds to grid
            colModel: [
                { name: "Id", width:"100px", formatter: formatOperations },
                { name: "Name", width: "100px"  },
                { name: "Description", width:"150px" },
                { name: "Abv", width: "100px"  },
                { name: "IBU", width:"170px" },
                { name: "StyleId", width: "100px"  },
                { name: "Status", width: "100px"  },
                { name: "CreateDate", width: "130px"  },
                { name: "UpdateDate", width: "130px"  },
                { name: "IsOrganic", width: "100px"  },
                { name: "CategoryName", width:"150px" }
            ],
            height: '100%',
            rowNum: 50,
            viewrecords: true,
            caption: '<button id="btnFilter" type="submit" onclick="showFilter(); return false" class="btn btn-primary" style="margin-right: 10px;" title="Filter" ><img src="../Content/Images/Filter.png" style="height: 20px;width: 20px;"></button>Beer List <label id="lblFilterText" style="color:Black;margin-left:10px;"></label>',
            emptyrecords: 'No records',

            pager: jQuery('#pager'),
            rowList: [50],
            jsonReader:
            {
                root: "rows",
                page: "page",
                total: "total",
                records: "records",
                repeatitems: false,
                Id: "0"
            },
            autowidth: false,
        })
        .navGrid('#pager',
        {
            edit: false,
            add: false,
            del: false,
            search: false,
            refresh: true,
            closeAfterSearch: true,
            closeAfterReset: true,
            searchtext: "Filter",
            refreshtext: "Refresh"
        });
}

function formatOperations(cellValue, options, rowdata, action) {
    return "<a href='BeerDetails?Id=" + cellValue + "'><u>" + cellValue + "</u></a>";
}

function LoadBeerData(gridData) {
    // set the new data
    $('#BeerGridList').jqGrid('setGridParam', {
               datatype: 'local',
               data: gridData.rows,
               jsonReader:
               {
                   page: gridData.page,
                   total: gridData.total,
                   records: gridData.records
               }
    }).trigger('reloadGrid');    
}
function OnFailure(data) {
    alert('HTTP Status Code: ' + data.param1 + '  Error Message: ' + data.param2);
}  

function showFilter() {
    $("#divtblBeerFilter").show();
    $("#m_txtBeerId").val(id);
    $("#m_txtBeerName").val(name);
    $("#m_txtBeerStyleID").val(styleId);
    if (status == "") {
        $("#m_ddlBeerStatus").val("choose");
    }
    else {
        $("#m_ddlBeerStatus option:contains(" + status + ")").attr('selected', 'selected');
    }
    if (isOrganic == "") {
        $("#m_ddlBeerOrganic").val("choose");
    }
    else {
        $("#m_ddlBeerOrganic option:contains(" + isOrganic + ")").attr('selected', 'selected');
    }
}

function hideFilter() {
    $("#divtblBeerFilter").hide();
    $("#lblErrorText").text("");
}

function deleteFilter() {
    $.ajax({
        url: "GetAllBeerList",
        type: "Get",
        data: { sort: "", page: 1, rows: 50, searchString: "" },
        cache: false,
        async: true,
        success: LoadBeerData,
        OnFailure: OnFailure
    });
    clearFilterData();
    hideFilter();
}

function saveFilter() {
    var filterData = "";
    $("#lblFilterText").text("Filter applied on :: ");
    if ($("#m_txtBeerId").val() != "") {
        filterData = filterData + "&ids=" + $("#m_txtBeerId").val();
        id = $("#m_txtBeerId").val();
        $("#lblFilterText").text($("#lblFilterText").text() + "Ids= " + $("#m_txtBeerId").val() + ", ")
        filterCount++;
    }
    if ($("#m_txtBeerStyleID").val() != "") {
        filterData = filterData + "&styleId=" + $("#m_txtBeerStyleID").val();
        styleId = $("#m_txtBeerStyleID").val();
        $("#lblFilterText").text($("#lblFilterText").text() + "StyleId= " + $("#m_txtBeerStyleID").val() + ", ")
    }
    if ($("#m_ddlBeerStatus option:selected").val() != "choose") {
        filterData = filterData + "&status=" + $("#m_ddlBeerStatus option:selected").val();
        status = $("#m_ddlBeerStatus option:selected").val();
        $("#lblFilterText").text($("#lblFilterText").text() + "Status= " + $("#m_ddlBeerStatus option:selected").val() + ", ")
        filterCount++;
    }
    if ($("#m_ddlBeerOrganic option:selected").val() != "choose") {
        filterData = filterData + "&isOrganic=" + $("#m_ddlBeerOrganic option:selected").val();
        isOrganic = $("#m_ddlBeerOrganic option:selected").val();
        $("#lblFilterText").text($("#lblFilterText").text() + "& IsOraganic= " + $("#m_ddlBeerOrganic option:selected").text())
    }
    if (filterCount > 1) {
        $("#lblErrorText").text("Ids & status cant not filterd together in BETA version");
        filterCount = 0;
        $("#lblFilterText").text("");
    }
    else {
        $.ajax({
            url: "GetFilteredBeerList",
            type: "Post",
            data: { page: 1, filterStr: filterData },
            cache: false,
            async: true,
            success: LoadBeerData,
            OnFailure: OnFailure
        });
        hideFilter();
        filterCount = 0;
    }
}

function clearFilterData()
{
    $("#m_txtBeerId").val("");
    $("#m_txtBeerName").val("");
    $("#m_txtBeerStyleID").val("");
    $("#lblFilterText").text("");
    id = "";
    name = "";
    styleId = "";
    status = "";
    isOrganic = "";
    $("#lblErrorText").text() = "";
}