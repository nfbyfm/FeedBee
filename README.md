![FeedBee Banner](https://raw.githubusercontent.com/nfbyfm/FeedBee/master/doc/FeedBee_SP.png)

# FeedBee

This program is a simple RSS-Reader, which has been written for Windows 7 and up. You can download the latest release [here](https://github.com/nfbyfm/FeedBee/releases).

![Screenshot FeedBee](https://raw.githubusercontent.com/nfbyfm/FeedBee/master/doc/MainApp.jpg)

This program lets you create various groups of feeds for web-comics, tumblr-pages, youtube-channels (without having to subscribe with an actual google-account), rss-feeds in general and mangarock-pages per default. 
This software also allows you to create feeds from webpages, that don't have any direct rss-functionalities (just like the mangarock-pages for example). For more info on that, see part about [Webpage-Feed-Definitions](#webpage-feed-definitions).




# Usage

## file-menu

The 'file'-menu contains all functions concerning the saving an loading of feed-lists and the closing of the application.

![Screenshot file-menu](https://raw.githubusercontent.com/nfbyfm/FeedBee/master/doc/file-menu.jpg)


### open file

The application usually saves the list of feeds and feedgroups into an xml-file. With this function you can select the xml-file that the program shall open by yourself.


### save file

Same as the 'open file'-function you can manually save the xml file. This can happen automatically if the setting '[load / save list](#FeedsSetting2)' is activated (you have to tell the program where this file can be found).

### import file
You can import feeds in form of an opml- or txt-file. In case of the txt-file: Each line will get treated as a web-url. If the site doesn't exist / can't get reached the program will ignore the line and go to the next one, until the end of the list is reached.

### export file
Same as with the import, the application can generate a simple list of all feed-links or an opml-file.

### close program

If the '[load / save list](#FeedsSetting2)'-setting is activated, the program will save the currently loaded feed-list into the specified xml-file. For this to happen, you have to click the menu-item (or 'ctrl+q'). If you close the application with 'alt+f4' or the red cross in the upper right part of the main-window, no changes / updates will get written into the xml-file.


## feed-menu

The 'feed'-menu contains all functions which are applicable to every feedgroup / every single feed.

![Screenshot feed-menu](https://raw.githubusercontent.com/nfbyfm/FeedBee/master/doc/feed-menu.jpg)


### add a new feed

Copy the webpage or rss-url into memory (ctrl+c) and then click Feeds-> add new feed (or ctrl+n) in FeedBee. A dialog pops up which will show you which feeds could be found in the given url. 

![Screenshot Add new feed](https://raw.githubusercontent.com/nfbyfm/FeedBee/master/doc/add_new_feed.jpg)

Alternitavely you can also simply start the dialog, paste the webpage-url into the upper textbox and then click 'check for feeds'. Usually the programm will try to find feeds before showing the dialog 
if there's text in memory (If showing the dialog is a bit slow it is because of this search for possible feeds). 

Afterwards you can select one of the shown feeds and then click 'next'. A new Dialog will show up, asking into which group you want to add the feed. 
You can choose from either an existing group or create a new group.

![Screenshot select group](https://raw.githubusercontent.com/nfbyfm/FeedBee/master/doc/select_feedgroup.jpg)

These groups will be displayed in the treeview as nodes. If you choose to create a new group, you can select if it is a feed-group that can be considered nsfw. If you set is to true and the setting
'[update nsfw feeds](#FeedsSetting3)' is set to false, the feeds within the group won't get updated (if an update gets triggered).


### update feeds

By hitting 'f5' or clicking on the according menu-item, you can trigger a update for all feeds. 

### cancel update

If you have triggered an update, the 'cancel-update'-function get enabled. By either clicking the menu-item 

### mark feeds

With this function you can mark all unread feed-items as read (hit 'f7' or click on the menu-item). Alternitavely you can first select a feed or feedgroup and the right-click to open the contextmenu. With this one you can mark a group or a single feed as read.

### open (unread) feeds

This function allows you to open all unread feed-items at once. Either click on the menu-item or hit 'f6'.

### edit nodes

By first clicking on a feed or feedgroup and then right-clicking you can choose to edit the selected node. In case of a feed a dialog will pop up, letting you edit the name, the path to the icon and whether or not the webbrowser is supposed to directly load a webpage when one of the feed-items gets selected.

![Screenshot edit feed](https://raw.githubusercontent.com/nfbyfm/FeedBee/master/doc/edit_feed.jpg)

In case of a feed-group you can change the name and icon path as well. Additionally the pop-up dialog lets you set the feedgroup as 'nsfw' (see setting '[update nsfw feeds](#FeedsSetting3)') and, if you wish, you can set the icon-path for all child-feeds to the same image as teh group's.

![Screenshot edit feed](https://raw.githubusercontent.com/nfbyfm/FeedBee/master/doc/edit_feedgroup.jpg)




# extras-menu

The 'extras'-menu contains the settings, an about-function (which shows informations about the application) and the Webpage-Feed-Definitions-Editor.

![Screenshot extras-menu](https://raw.githubusercontent.com/nfbyfm/FeedBee/master/doc/extras-menu.jpg)

## Settings

The settings are currently divided into two categories: 'Feeds' and 'View'.

### Feeds

![Screenshot feed-settings](https://raw.githubusercontent.com/nfbyfm/FeedBee/master/doc/settings_feeds.jpg)


| Number | Name | Description |
|--------|------|-------------|
|#<a name="FeedsSetting1"></a> 1      | folder of youtube-dl     | if you have a copy of the youtube-dl.exe [homepage](https://rg3.github.io/youtube-dl/), [github-page](https://github.com/rg3/youtube-dl/), you can select the path for the program here. When subscribed to a youtube-channel, a download-button will appear near the url, if one of the feed-items (single video) gets selected.   | 
|#<a name="FeedsSetting2"></a> 2      | load / save list     | If set to true (selected), the programm will try to load upon start and save upon exit all the feedgroups to the specified file.  | 
|#<a name="FeedsSetting3"></a> 3      | update nsfw     | If set to false (not checked), feedgroups which are marked as nsfw will not get updated. Otherwise all feeds will get updated. | 
|#<a name="FeedsSetting4"></a> 4      | update upon load     | if set to true, the programm will automatically start the update-routine upon startup.  | 
|#<a name="FeedsSetting5"></a> 5      | feed definitions     | filepath to the webpage-feed-definitions-file (will get saved / loaded upon start / exit of the program).   | 



### View

![Screenshot view-settings](https://raw.githubusercontent.com/nfbyfm/FeedBee/master/doc/settings_view.jpg)


| Number | Name | Description |
|--------|------|-------------|
|#<a name="ViewSetting1"></a> 1      |  expand Treeview    |  Expands all the nodes (Feedgroups) of the treeview after each update     | 
|#<a name="ViewSetting2"></a> 2      |  don't display IFrames    |  In the integrated Webbrowser: don't show integrated IFrames (usually containing advertisments) if this option is activated | 
|#<a name="ViewSetting3"></a> 3      |  display icons    |  Each feedgroup and single feed can have it's own icon. If this setting is activated the application will try to download these images and save them to the specified folder (which greatly increses the speed of the application).    | 





### Webpage-Feed-Definitions

Webpages like mangarock don't have any rss-functionalities per se. The links to a chapter of a manga / comic however, has alway the same html-identifier. In order to find this id/class, you open the webpage you want to create a feed from and right-click on one of the 'feed-items'.
If you then click on 'inspect-element' (in firefox for instance), the browser will show you the html-code of the selected element. In case of a chapter-link on one of the mangarock-overview-page this will look something like this:

![Screenshot classID-mangarock](https://raw.githubusercontent.com/nfbyfm/FeedBee/master/doc/webpagedef_inspectItem_browser.jpg)

In order for the application to generate a feed-item out of this, you'll have to find the identifier of the item itself and it's publishing date. You can then enter them into the editor-mask as follows:

![Screenshot Webpage-Feed-Definition-Editor](https://raw.githubusercontent.com/nfbyfm/FeedBee/master/doc/edit_webpagefeeddefinitions.jpg)

Underneath the webpage-feed-definition's properties themselves, you can enter the url of a future feed you'd want to add and test if the application can actually create / find feed-items for the given webpage.

After you're done / were able to test a feed-url, you can click 'add to list' and then 'save'. You should now be able to add / generate feeds from webpages, that don't have any rss-functionalities.
