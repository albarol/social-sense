# Socialsense - Crawler for main social networks   

Author: Alexandre Barbieri (fakeezz)

## About

The project **social-sense** is a gateway for search in social networks. 
Now exists support to following channels(Facebook, Twitter, GoogleBlogs, GoogleNews, GoogleSearch, GooglePlus, Bing, Yahoo! Search and Digg).


## Examples

### Facebook

Get public comments from users about something you like.

```c#
var engine = EngineFactory.Facebook();
var results = engine.Search(new Query 
{ 
  Term = "c#", 
  MaxResult = 10, 
  Language = Language.English, 
  Country = Country.UnitedStatesOfAmerica 
});
```

### Google Sites

Get public information from sites you want know

```c#
var engine = EngineFactory.GoogleSites();
var results = engine.Search(new Query 
{ 
  Term = "c#", 
  MaxResult = 10, 
  Language = Language.BrazilianPortuguese,
  Country = Country.Brazil 
});
```