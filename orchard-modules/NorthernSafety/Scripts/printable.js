/*
$Id: Printable.js,v 1.4.2.1 2008/12/11 14:17:29 baileyj Exp $
Author: Jeremy Bailey
Date: 11/09/2005
Company: SourceOne Network, Inc
Description: 
Prints the content of the page opener using Javascript. 
This prevents the query string and FORM posting problem of the the past.

The basic idea is that we grab all the HTML from our parent, then look for the <crawl start>/<crawl stop> tags. 
Everything between them is GOOD content, everything outside is header/footer.  We do some string magic and write to 
the contents of our own div, and voila, we have just the content! 
*/

function NSPrintable() {
    var PrintWindow;
    NSPrintableWin = window.open("", "Printable", "width=720,height=600,left=50,scrollbars,resizable");
    PrintWindow = NSPrintableWin.document.open();
    PrintWindow.writeln('<html>\n<head>\n<title>' + document.title + ' </title>');
    PrintWindow.writeln('<link href="/css/pages/product_details_print.css" rel="styleSheet" type="text/css">');
    PrintWindow.writeln('</head>\n<body>');
    PrintWindow.writeln('<div id="NSPrintableToolbar" class="NSPrintableToolbar">');
    PrintWindow.writeln('<a href="javascript:void(0);" OnClick="window.opener.NSPrintablePrint();">Print This Page</a>&nbsp;&nbsp;');
    PrintWindow.writeln('</div>');
    PrintWindow.writeln('<div id="header"><h1 class="logo"><img src="/images/logo.gif" alt="Northern Safety Co., Inc. Safety and Industrial Supplies" border="0"></h1><p id="callCenterSchedule"><span class="phone">1-800-571-4646</span><br>For sales &amp; customer service<br><em>7:30 am - 8:00 pm ET, Mon-Fri</em></p><p id="contactAddress">Northern Safety Co., Inc.<br>PO Box 4250, Utica, NY 13504-4250<br><span class="phone">Phone:</span> 1-800-571-4646<br><span class="phone">Fax:</span> 1-800-635-1591</p></div><div style="margin:10px;" class="clear"></div>');
    PrintWindow.writeln('<div id="NSPrintableContent" class="NSPrintableContent"> ' + NSPrintableCopyContent() + '</div>');

    PrintWindow.writeln('</body>\n</html>\n');

    PrintWindow = NSPrintableWin.document.close();
    // We had a problem with the disable link script running too soon, before the links populated
    // So, we "delay" it by shoving it through setTimeout();
    window.setTimeout('NSPrintableDisableActions()', 0);
    NSPrintableWin.focus();
}

function NSPrintable2() {
    var PrintWindow;
    NSPrintableWin = window.open("", "Printable", "width=900,height=600,left=50,scrollbars,resizable");
    PrintWindow = NSPrintableWin.document.open();
    PrintWindow.writeln('<html>\n<head>\n<title>' + document.title + ' </title>');
    PrintWindow.writeln('<link href="/css/checkout.css" rel="styleSheet" type="text/css">');
    PrintWindow.writeln('<link href="/css/pages/checkout5_print.css" rel="styleSheet" type="text/css">');
    PrintWindow.writeln('</head>\n<body>');
    PrintWindow.writeln('<div id="NSPrintableToolbar" class="NSPrintableToolbar">');
    PrintWindow.writeln('<a href="javascript:void(0);" OnClick="window.opener.NSPrintablePrint();">Print This Page</a>&nbsp;&nbsp;');
    PrintWindow.writeln('</div>');
    PrintWindow.writeln('<div id="printHeader"><h1 class="logo"><img src="/images/logo.gif" alt="Northern Safety Co., Inc. Safety and Industrial Supplies" border="0"></h1></div>');
    PrintWindow.writeln('<div id="NSPrintableContent" class="NSPrintableContent"> ' + NSPrintableCopyContent() + '</div>');

    PrintWindow.writeln('</body>\n</html>\n');

    PrintWindow = NSPrintableWin.document.close();
    // We had a problem with the disable link script running too soon, before the links populated
    // So, we "delay" it by shoving it through setTimeout();
    window.setTimeout('NSPrintableDisableActions()', 0);
    NSPrintableWin.focus();
}

function NSPrintableCopyContent() {
    var Content, startpos, endpos, offset, starttag, endtag;
    // IE and Firefox treat the content differently, so we need different tags and offsets
    if (document.all) {
        starttag = '<PRINT start';
        endtag = '<PRINT end';
        offset = 13;
    } else {
        starttag = '<print start';
        endtag = '<print end';
        offset = 16;
    }
    // Grab the parent's content and find our start/end tags
    Content = document.body.innerHTML;
    startpos = Content.indexOf(starttag);
    endpos = Content.indexOf(endtag);
    if (startpos < 0 || endpos < 0) {
        // return a nice error if something went wrong
        return "Error capturing text";
    } else {
        // reset the content
        return Content.substr(startpos + offset, endpos - startpos - offset);
        // Disable any links (they shouldn't be followed within the popup)
    }
}
function NSPrintablePrint() {
    NSPrintableWin.print();
    NSPrintableWin.close();
}
function NSPrintableDisableActions() {
    // Loop through the links, disable them
    for (i = 0; i < NSPrintableWin.document.links.length; i++) {
        if (NSPrintableWin.document.links[i].parentNode.id != 'NSPrintableToolbar') {
            // Only disable other links
            NSPrintableWin.document.links[i].onclick = function() { return false };
            NSPrintableWin.document.links[i].href = "javascript:void(0)";
        }
    }
    // Loop through the forms, disable the buttons (we don't disable the whole form, since it looks bad)
    for (var i = 0; i < NSPrintableWin.document.forms.length; i++) {
        for (var j = 0; j < NSPrintableWin.document.forms[i].elements.length; j++) {
            if (NSPrintableWin.document.forms[i].elements[j].type == 'submit' || NSPrintableWin.document.forms[i].elements[j].type == 'button') {
                // submit buttons and regular buttons are cadidates
                // Note: I tried altering the action or onSubmit() but that didn't seem to work
                NSPrintableWin.document.forms[i].elements[j].disabled = true;
            }
        }
    }
}