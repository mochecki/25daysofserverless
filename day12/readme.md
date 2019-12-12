# Challenge 12: Caching

## Implementation
**Function App** with [Oktokit](https://github.com/octokit) to download the gist with multiple files (one file per card) and cache in **Redis Cache**. 
[Markdig](https://github.com/lunet-io/markdig_) to convert markdown file to HTML.
**API Management (Consumption Plan)** with external **Redis Cache** to cache HTML responses for all cards.

![API Management](images/apimanagement.png)
![API Management-cache config](images/apimanagement-cache.png)

![Main Page](images/mainpage.png)
![Card](images/card.png)
