/*
-control
    label => lb
    div => d
    i => i
    h1 => h1
    hr => hr
    Content => c
    ScriptManager => sm
    UpdatePanel => up
    Button => btn
    TextBox => tb
    ComboBox => cb
    GridView => gd
    ul => ul
    SqlDataSource => sqls
    input => in
    image => img
    body => bd
    form => fm
    span => sp
    FileUpload => fu
    img => img
 -var
    int => i
    string => s
    boolean => b
    number => n
    StringBuilder => sb
    DirectoryInfo => di
 */


(function () {
    var sAdjustmentDelegates = [];
    function AddAdjustmentDelegate(adjustmentDelegate) {
        sAdjustmentDelegates.push(adjustmentDelegate);
    }
    //root.master에서 global event로 설정(크기 재조정)
    function onControlsInitialized(s, e) {
        adjustPageControls();
    }
    function onBrowserWindowResized(s, e) {
        adjustPageControls();
    }
    function adjustPageControls() {
        for (var i = 0; i < sAdjustmentDelegates.length; i++) {
            sAdjustmentDelegates[i]();
        }
    }

    //크기 재조정
    function adjustGridView() {
        gdGridview.AdjustControl();
    }

    function onRightMenuItemClick(s, e) {
        //우측 상단 판넬 클릭
        //if (e.item.name === "ToggleRightPanel") {
        //    toggleRightPanel();
        //    e.processOnServer = false;
        //}

        if (e.item.name === "AccountItem")
            e.processOnServer = false;
    }
 
    function onGridViewInit(s, e) {
        AddAdjustmentDelegate(adjustGridView);
    }
    function showLoadingPanelDemo() {
        console.log("showLog");
        var x = document.getElementById("thick");
        var bg = document.getElementById("backPopup");
        x.style.display = "block";
        bg.style.display = "block";
    }

    function hideLoadingPanelDemo() {
        console.log("hideLog");
        var x = document.getElementById("thick");
        var bg = document.getElementById("backPopup");
        x.style.display = "none";
        bg.style.display = "none";
    }
    $(document).ready(function () {
        if (window.location.pathname == "/Page/Management/patentManagementInput.aspx") { //특허 등록 페이지에서만 뜨도록
            //저장 완료 상태 표시 가리기
            var dAlert_save = document.querySelector('.dDataalert_save');
            var save_visible = document.defaultView.getComputedStyle(dAlert_save).getPropertyValue("visibility");

            if (save_visible.toString() == "visible") {
                $('.dDataalert_save').css('visibility', 'hidden');
                save_visible = document.defaultView.getComputedStyle(dAlert_save).getPropertyValue("visibility");
            }

            var dAlert_delete = document.querySelector('.dDataalert_delete');
            var delete_visible = document.defaultView.getComputedStyle(dAlert_delete).getPropertyValue("visibility");

            if (delete_visible.toString() == "visible") {
                $('.dDataalert_delete').css('visibility', 'hidden');
                delete_visible = document.defaultView.getComputedStyle(dAlert_save).getPropertyValue("visibility");
            }
        }
    });

    window.onload = function () {
        console.log("창 오픈!");



        //메뉴바 노드 네모/체크 표시
        $('.node').parents().attr('id', 'node');
        $('.selectedNode').parent().attr('id', 'select');
        
        $('#node').toggleClass('defaultNode');
        $('#select').toggleClass('change');
    }
    //function menuBtnClick() {
    //    $('#HeaderPanel_menuBtn').toggleClass('menuBtn_collapse');
    //    toggleLeftPanel();

    //    console.log("leftpanel state1", leftPanel.GetVisible());
    //    console.log("leftpanel state-menuBtn", menuBtn.GetChecked());
    //    //menuBtn.SetChecked();
    //}
    //function toggleLeftPanel() {
    //    if (leftPanel.IsExpandable()) {
    //        leftPanel.Toggle();
    //        console.log("leftpanel state2", leftPanel.GetVisible());
    //    }
    //    else {
    //        leftPanel.SetVisible(!leftPanel.GetVisible());
    //        adjustPageControls();
    //        menuBtn.SetChecked(leftPanel.GetVisible());
    //        console.log("leftpanel state3", leftPanel.GetVisible());
    //    }
    //}
    function leftPanelInit(s, e) {
        console.log("init", menuBtn.GetChecked());
    }
    function newBtnClick(s, e) {
        var name = btnNew.GetImageUrl().slice(-14);
        console.log(name);
        if (name == 'button_add.svg')
            alertsetting(2);
        else
            alertsetting(4);        
    }
    function expandBtnClick(s, e) {
        $('#LeftPanel_ExpandBtn').toggleClass('foldBtn_check');

        if (jQuery("#LeftPanel_ExpandBtn").attr("class").includes("check"))
            treeViewContainer.CollapseAll();
        else 
            treeViewContainer.ExpandAll();
    }
    function menuBtnClick(s, e) {
        $('#HeaderPanel_menuBtn').toggleClass('menuBtn_collapse');
        if (jQuery("#HeaderPanel_menuBtn").attr("class").includes("collapse")) {
            //leftPanel.Expand();
            leftPanel.SetVisible(false);
            //localStorage.setItem('menuState', false);
            setCookie("menuState", "false", 1);
        }
        else {
            //leftPanel.Collapse();  
            leftPanel.SetVisible(true);
            //localStorage.setItem('menuState', true);
            setCookie("menuState", "true", 1);
        }
        //alert(localStorage.menuState);

    }
    function setCookie(name, value, exp, path, domain) {
        var date = new Date();
        date.setTime(date.getTime() + exp * 24 * 60 * 60 * 1000); // 일
        var cookieText = escape(name) + '=' + escape(value);
        //cookieText += (exp ? '; EXPIRES=' + exp.toUTCString() : '; EXPIRES=' + date.toUTCString());
        cookieText += (path ? '; PATH=' + cookiePath : '; PATH=/');
        cookieText += (domain ? '; DOMAIN=' + cookieDomain : '');
        document.cookie = cookieText;
    }
    /************************
    //*> Manage Form
   ************************/

    //------------------------------------------------------------buttons    
    function OnExportClickExcel(s, e) {//출력버튼
        gdGridview.ExportTo(ASPxClientGridViewExportFormat.Xlsx);
    }

    function OnExportClickPdf(s, e) {//출력버튼
        gdGridview.ExportTo(ASPxClientGridViewExportFormat.Pdf);
    }

    function OnExportClickWord(s, e) {//출력버튼
        gdGridview.ExportTo(ASPxClientGridViewExportFormat.Docx);
    }

    function alertsetting(iOp) {//상태 팝업 띄우기
        $("#dAlert").css('visibility', 'visible');
        if (iOp == 0) {
            $("#iAlerticon").attr('class', 'fa fa-eraser fa-5x');
            lbAlerttext.SetText("초기화");
        }
        /*else if (iOp == 1) {
            $("#iAlerticon").attr('class', 'fa fa-save fa-5x');
            lbAlerttext.SetText("저장 완료");
        }*/
        else if (iOp == 2) {
            $("#iAlerticon").attr('class', 'fa fa-plus fa-5x');
            lbAlerttext.SetText("신규 추가");
        }
        else if (iOp == 3) {
            $("#iAlerticon").attr('class', 'fa fa-trash-o fa-5x');
            lbAlerttext.SetText("삭제 완료");
        }
        else if (iOp == 4) {
            $("#iAlerticon").attr('class', 'fa fa-times fa-5x');
            lbAlerttext.SetText("취소");
        }
        else if (iOp == 5) {
            $("#iAlerticon").attr('class', 'fa fa-times fa-5x');
            lbAlerttext.SetText("미입력");
        }
        $("#dAlert").fadeIn(400);
        setTimeout(function () {
            $("#dAlert").fadeOut(300);
        }, 1500);
    }

    //------------------------------------------------------------gridview

    function sortandfocus() {//정렬 후 포커싱
       //setTimeout(function () {
            //gdGridview.SetFocusedRowIndex(0);
       //}, 300);
    }

    function OnGetRowValues_patent(sValues) {//특허관리 입력폼 바인딩
        if (sValues[0] != null) {
            console.log('patent focus values:', sValues[0]);
            tbSeqTxt.SetText(sValues[0]);//바인딩
            tbNameTxt.SetText(sValues[1]);
            tbInventorTxt.SetText(sValues[2]);
            tbNumTxt.SetText(sValues[3]);
            tbAppnumTxt.SetText(sValues[4]);
            cbDivTxt.SetText(sValues[5])
            deFilldate.SetDate(sValues[6]);
            deRegdate.SetDate(sValues[7]);
        }
        else {
            tbSeqTxt.SetText("");//바인딩
            tbNameTxt.SetText("");
            tbInventorTxt.SetText("");
            tbNumTxt.SetText("");
            tbAppnumTxt.SetText("");
            cbDivTxt.SetText("")
            deFilldate.SetDate(null);
            deRegdate.SetDate(null);
        }
    }

    function Manage_FocusedRowChanged(s, e) {//pc 포커싱 설정
        setPagerTop(s, e);
        gdGridview.GetRowValues(gdGridview.GetFocusedRowIndex(), 'ip_seq;ip_name;ip_inventor;ip_num;ip_appnum;ip_division;ip_fillDate;ip_regDate', OnGetRowValues_patent);
        if (gdGridview.GetFocusedRowIndex() == null || gdGridview.GetFocusedRowIndex() == -1) {
            lbPdfLab.SetText('PDF파일');
            $('#dFilelistout').css('display', 'none');
            $('#dPaginationwrap').css('display', 'none');
        }
        else {
            $('#dFilelistout').css('display', 'block');
            $('#dPaginationwrap').css('display', 'block');
            iPos = 0;
            setTimeout(function () {
                document.getElementsByClassName("btnViewfile")[0].click();
            }, 550);
                
        }
    }

    function setPagerTop(s,e){
        //var a = 0;
        //if ($('.dxgvGroupPanel_Office365').css('height') != null) {
        //    a = parseInt($('.dxgvGroupPanel_Office365').css('height').split('px')[0])+24;
        //}
        //$('#PageContent_dPagernum').css('top', parseInt($('.dxgvCSD').css('height').split('px')[0]) + a + 232 + 'px');
    }

    var asdasd = 0;

    function autoAddNewRow(s, e) {
        if (asdasd % 2 == 0) {
            alert((gdGridview.GetVisibleRowsOnPage() - 1) + "/" + gdGridview.GetFocusedRowIndex()+ "/" + asdasd);
            if ((gdGridview.GetVisibleRowsOnPage() - 1) == gdGridview.GetFocusedRowIndex() || gdGridview.GetFocusedRowIndex() == -1) {

                s.AddNewRow();
            }
        }
        asdasd++;
        asdasd = asdasd%2;
    }

    function inputinit(s, e) {
        s.AddNewRow();  
        s.AddNewRow();  
        s.AddNewRow();  
        s.UpdateEdit();
    }

    function input1(s, e) {
        alert("input1")
    }

    function input2(s, e) {
        alert("input2")
    }

    function input3(s, e) {
        alert("input3")
    }

    //------------------------------------------------------------filelist

    var iPos = 0;//파일리스트 위치 초기값은 0

    function pdfrefresh() {//pdf새로고침
        document.getElementsByClassName("btnViewfile")[0].click();
    }

    function filelisttext(s, e) {//파일리스트 슬라이드 생성 이벤트
        if (gdGridview.GetFocusedRowIndex() == null || gdGridview.GetFocusedRowIndex() == -1) {
            lbPdfLab.SetText('PDF파일');
            $('#dFilelistout').css('display', 'none');
            $('#dPaginationwrap').css('display', 'none');
        }
        else {
            $('#dFilelistout').css('display', 'block');
            $('#dPaginationwrap').css('display', 'block');
        }
        var sLists = tbFileslabel.GetText().split('/');
        $('#dFilelist').children().remove('.divNofiletext');
        if (gdGridview.GetFocusedRowIndex() != -1 && gdGridview.GetFocusedRowIndex() != null) {
            $('#ulSlider').children().remove('.dynamiclist');
            console.log('filelist values', sLists[0]);
            if (tbFileslabel.GetText() != '') {
                for (i = 0; i < sLists.length - 1; i++) {
                    filelist(sLists[i], sLists.length - 1, true);
                }
                lbPdfLab.SetText("PDF파일 (" + sLists[sLists.length - 1] + ")");
                if (sLists[sLists.length - 1] == "0") {
                    $('#dNext').css('display', 'none');
                    $('#dPrevious').css('display', 'none');
                }
                else {
                    $('#dNext').css('display', 'block');
                    $('#dPrevious').css('display', 'block');
                }
                slideready();
                console.log("slidepos" + sLists.length);
                setTimeout(function () {
                    if (iPos == 0) {
                        slidepos(0);
                    }
                    else if (iPos == sLists.length - 1) {
                        slidepos(iPos - 1);
                    }
                    else {
                        slidepos(iPos);
                    }
                }, 200);
            }
            if (sLists[0] == "0") {
                console.log("비어있음");
                var text = document.createElement('div');
                text.style.fontSize = '11px';
                text.style.fontWeight = '500';
                text.style.color = 'black';
                text.className = "divNofiletext";
                text.style.display = 'block';
                text.style.width = '50%';
                text.style.marginLeft = '25%';
                text.style.marginRight = '25%';
                text.style.boxSizing = "border-box";
                text.style.textAlign = "center";
                text.innerHTML = '업로드한 파일이 없습니다';
                document.getElementById('dFilelist').appendChild(text);
                $('#dFilelist ul#ulSlider').width(270);
            }
        }
    }

    function filelist(name, len, a) {//파일리스트 동적html 추가
        //파일 박스
        var newDIV = document.createElement('div');
        newDIV.className = 'dynamiclist';
        newDIV.cssFloat = 'left';
        newDIV.style.display = 'block';
        newDIV.style.width = (70 / len).toString() + '%';
        newDIV.style.marginLeft = (15 / len).toString() + '%';
        newDIV.style.marginRight = (15 / len).toString() + '%';
        newDIV.style.paddingRight = '0px';
        newDIV.style.textAlign = 'center';
        //newDIV.style.boxSizing = "border-box";
        //newDIV.style.backgroundColor = 'rgba(' + getRand(70, 200) + ',' + getRand(70, 200) + ',' + getRand(70, 200) + ',1)';
        newDIV.style.height = "100%";
        newDIV.style.verticalAlign = "middle";
        var div = document.createElement('div');
        div.style.width = "100%";
        div.style.height = "auto";
        div.style.lineHeight = "53px";
        //파일 명
        var text = document.createElement('label');
        text.style.maxWidth = '100%';
        text.style.fontSize = '9pt';
        text.style.fontWeight = '500';
        text.style.color = '#0071bc';
        text.style.textDecoration = 'underline'
        text.style.display = "inline-block";
        text.style.whiteSpace = "normal";
        text.style.overflow = "hidden";
        text.style.textAlign = "center";
        text.style.wordWrap = "break-word";
        text.style.textOverflow = "ellipsis";
        text.style.verticalAlign = "middle";
        text.style.lineHeight = "11px";
        text.innerHTML = name;
        text.onclick = function () {
            tbFilename.SetText(name); slidepos(iPos); window.open('http://localhost:52732/Content/ipmanagepdf/' + tbSeqTxt.GetText() + '/' + tbFilename.GetText(), 'pop' + tbSeqTxt.GetText() + '/' + tbFilename.GetText(), 'top=10, left=10, width=500, height=700, status=no, menubar=no, toolbar=no, resizable=no');
        };
        div.append(text);
        //파일 삭제버튼(pc페이지만)
        if (a) {
            var xdiv = document.createElement('p');
            xdiv.className = "";
            xdiv.innerHTML = "파일 삭제"
            xdiv.style.zIndex = '100';
            xdiv.style.width = '70px';
            xdiv.style.height = '20px';
            xdiv.style.fontSize = '9pt';
            xdiv.style.margin = "0px auto";
            xdiv.style.border = '1px solid #B3B3B3';
            xdiv.style.backgroundColor = '#F2F2F2';
            xdiv.style.verticalAlign = "middle";
            xdiv.style.color = 'black';
            xdiv.style.lineHeight = "20px";
            xdiv.onclick = function () {              
                var sFilename = $(this).parent().text();
                tbFilename.SetText(sFilename);
                document.getElementsByClassName("btnPdfdel")[0].click();
            };
            div.append(xdiv);
        }
        newDIV.append(div);
        document.getElementById('ulSlider').appendChild(newDIV);
    }

    function getRand(iMin, iMax) {//파일 박스 색상 랜덤 함수
        if (iMin >= iMax) return false;
        return ~~(Math.random() * (iMax - iMin + 1)) + iMin;
    };

    function slideready() {//파일리스트 슬라이드 객체 추가 후 세팅
        var iTotalSlides = $('#dFilelist ul div.dynamiclist').length;
        var nSliderWidth = $('#dFilelist').width();
        $('#dFilelist ul#ulSlider').width(nSliderWidth * iTotalSlides);
        $('#dFilelist ul#ulSlider').css('left', '0px');

        $('#dPaginationwrap ul').empty();

        $.each($('#dFilelist ul div.dynamiclist'), function () {
            var li = document.createElement('li');
            li.onclick = function () {
                $('#dPaginationwrap ul li').removeClass('active');
                $(this).addClass('active');
                slidepos($('#dPaginationwrap ul li').index(this));
            };
            //console.log("add li");
            var childCount = $('#dPaginationwrap ul').children().length;
            var childCount = childCount + 1;

            li.id = childCount.toString();
            
            $('#dPaginationwrap ul').append(li);

            /*if (childCount > 14) { //첫 로드 시 파일 리스트 점 15개로 유지
                var id = childCount;
                id = id.toString();
                id = '#' + id;
                $(id).css('display', 'none');
            }*/
            //console.log(childCount);
        });
        

        countSlides();

        pagination();
    }

        $(document).ready(function () {
        //슬라이드 다음 버튼
        $('#dNext').click(function () {
            slideRight();
        });
        //슬라이드 이전 버튼
        $('#dPrevious').click(function () {
            slideLeft();
        });
        //호버 css적용
        $('#dFilelist').hover(
            function () { $(this).addClass('active'); },
            function () { $(this).removeClass('active'); }
        );
    });


    function slidepos(iP) {//해당 번호의 슬라이드로 이동
        var nSliderWidth = $('#dFilelist').width();
        iPos = iP;
        $('#dFilelist ul#ulSlider').css('left', -(nSliderWidth * iPos));
        countSlides();
        pagination();
    }
    function dotDisplay(iPos) {
        console.log(iPos);
        if (iPos == 0) {
            var childCount = $('#dPaginationwrap ul').children().length;
            console.log('cc' + childCount);
            if (childCount > 14) { //첫 로드 시 파일 리스트 점 15개로 유지 //내일 마저 하기

                for (i = 15; i <= childCount; i++) {
                    var id = i;
                    id = id.toString();
                    id = '#' + id;
                    $(id).css('display', 'none');
                }
            }
        }
        else if (iPos % 13 == 0) {

            for (i = iPos; i < iPos + 14; i++) {//오류수정하기
                //다음 14개 점 보여주기
                var next_id = i;
                next_id = next_id.toString();
                next_id = '#' + next_id;
                $(next_id).css('display', 'inline-block');

                //이전 점들 없애기
                var prev_id = i - 13;
                prev_id = prev_id.toString();
                prev_id = '#' + prev_id;
                $(prev_id).css('display', 'none');

            }

            console.log("14배수");
        }
    }

    function slideLeft() {//왼쪽 슬라이드로 이동
        var iTotalSlides = $('#dFilelist ul div.dynamiclist').length;
        var nSliderWidth = $('#dFilelist').width();
        iPos--;
        if (iPos == -1) { iPos = iTotalSlides - 1; }
        $('#dFilelist ul#ulSlider').css('left', -(nSliderWidth * iPos));
        //*> optional
        countSlides();
        pagination();
    }

    function slideRight() {//오른쪽 슬라이드로 이동
        var iTotalSlides = $('#dFilelist ul div.dynamiclist').length;
        var nSliderWidth = $('#dFilelist').width();
        iPos++;
        if (iPos == iTotalSlides) { iPos = 0; }
        $('#dFilelist ul#ulSlider').css('left', -(nSliderWidth * iPos));
        console.log(iPos);
        //dotDisplay(iPos);

        //*> optional 
        countSlides();
        pagination();
    }

    function countSlides() {//슬라이드 페이지(숫자)
        var iTotalSlides = $('#dFilelist ul div.dynamiclist').length;
        $('#dCounter').html(iPos + 1 + ' / ' + iTotalSlides);
    }

    function pagination() {//하단 슬라이드 페이지 매기기
        $('#dPaginationwrap ul li').removeClass('active');
        $('#dPaginationwrap ul li:eq(' + iPos + ')').addClass('active');
    }

    window.onControlsInitialized = onControlsInitialized;
    window.onBrowserWindowResized = onBrowserWindowResized;
    window.adjustPageControls = adjustPageControls;

    window.AddAdjustmentDelegate = AddAdjustmentDelegate;

    window.showLoadingPanelDemo = showLoadingPanelDemo;
    window.hideLoadingPanelDemo = hideLoadingPanelDemo;

    //patentManagement 관련
    window.Manage_FocusedRowChanged = Manage_FocusedRowChanged;
    window.sortandfocus = sortandfocus;
    window.filelisttext = filelisttext;
    window.setPagerTop = setPagerTop;
    window.OnExportClickExcel = OnExportClickExcel;
    window.OnExportClickPdf = OnExportClickPdf;
    window.OnExportClickWord = OnExportClickWord;
    window.alertsetting = alertsetting;
    window.autoAddNewRow = autoAddNewRow;
    window.input1 = input1;
    window.input2 = input2;
    window.input3 = input3;
    window.inputinit = inputinit;
    //추가해주기

    //leftpanel 관련
    window.leftPanelInit = leftPanelInit;
    window.newBtnClick = newBtnClick;
    window.expandBtnClick = expandBtnClick;
    window.menuBtnClick = menuBtnClick;
}) ();
