# FeedRead

This program is a simple RSS-Reader in Development. This software has been written for Windows 7 and up. You can download the latest release [here](https://github.com/nfbyfm/FeedRead/releases).

![Screenshot FeedRead](https://raw.githubusercontent.com/nfbyfm/FeedRead/master/doc/MainApp.jpg)

This program lets you create various groups of feeds for web-comics, tumblr-pages, youtube-channels (without a google-account), rss-feeds in general and mangarock-pages per default. 
In the future, this software should also be able to give the user the possibilty to generate feeds from webpages, that don't have any direct rss-functionalities (just like the mangarock-pages).

## Usage

### open file


### save file


### import file


### export file


### close program


### add a new feed

Copy the webpage or rss-url into memory (ctrl+c) and then click Feeds-> add new feed (or ctrl+n) in FeedRead. A dialog pops up which will show you which feeds could be found in the given url. 

![Screenshot Add new feed](https://raw.githubusercontent.com/nfbyfm/FeedRead/master/doc/add_new_feed.jpg)

Alternitavely you can also simply start the dialog, paste the webpage-url into the upper textbox and then click 'check for feeds'. Usually the programm will try to find feeds before showing the dialog 
if there's text in memory (If showing the dialog is a bit slow it is because of this search for possible feeds). 

Afterwards you can select one of the shown feeds and then click 'next'. A new Dialog will show up, asking into which group you want to add the feed. 
You can choose from either an existing group or create a new group.

![Screenshot select group](https://raw.githubusercontent.com/nfbyfm/FeedRead/master/doc/select_feedgroup.jpg)

These groups will be displayed in the treeview as nodes. If you choose to create a new group, you can select if it is a feed-group that can be considered nsfw. If you set is to true and the setting
'[update nsfw feeds](#FeedsSetting3)' is set to false, the feeds within the group won't get updated (if an update gets triggered).


### update feeds


### cancel update


### mark feeds


### open (unread) feeds


### edit nodes


## Settings

### Feeds

![Screenshot view-settings](https://raw.githubusercontent.com/nfbyfm/FeedRead/master/doc/settings_feeds.jpg)


| Number | Name | Description |
|--------|------|-------------|
|#<a name="FeedsSetting1"></a> 1      | folder of youtube-dl     | if you have a copy of the youtube-dl.exe (), you can select the path for the program here. When subscribed to a youtube-channel, a download-button will appear near the url, if one of the feed-items (single video) gets selected.   | 
|#<a name="FeedsSetting2"></a> 2      | load / save list     | If set to true (selected), the programm will try to load upon start and save upon exit all the feedgroups to the specified file.  | 
|#<a name="FeedsSetting3"></a> 3      | update nsfw     | If set to false (not checked), feedgroups which are marked as nsfw will not get updated. Otherwise all feeds will get updated. | 
|#<a name="FeedsSetting4"></a> 4      | update upon load     | if set to true, the programm will automatically start the update-routine upon startup.  | 
|#<a name="FeedsSetting5"></a> 5      | feed definitions     | filepath to the webpage-feed-definitions-file (will get saved / loaded upon start / exit of the program).   | 



### View

![Screenshot view-settings](https://raw.githubusercontent.com/nfbyfm/FeedRead/master/doc/settings_view.jpg)


| Number | Name | Description |
|--------|------|-------------|
|#<a name="ViewSetting1"></a> 1      |      |             | 





## Webpage-Feed-Definitions



## used icon(s)
<div>
	Icons made by <a href="https://www.freepik.com/" title="Freepik">Freepik</a> from <a href="https://www.flaticon.com/" title="Flaticon">www.flaticon.com</a> 
	are licensed by <a href="http://creativecommons.org/licenses/by/3.0/"title="Creative Commons BY 3.0" target="_blank">CC 3.0 BY</a>
</div>
