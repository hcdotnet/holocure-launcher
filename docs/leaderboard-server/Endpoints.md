# Server Database Endpoints

Documented below are the actual endpoints. For the database URL used by HoloCure, as well as the API key, auth location, and refresh location, see [Others.md](Others.md).

## `/{uid}.json?auth={auth_token}` (PUT)

* `uid`: TODO, `-4` by default, set when a certain response is received.

#### JSON Request Body:

```json
{
  "userName": "the username that should be displayed",
  "enemyDefeated": "",
  "character": "the ID of the character being used",
  "level": "the player's level",
  "uid": "uid",
  "date": "the year followed by the week, ex. '2022_5'"
}
```

## `{uid}/enemyDefeated.json?shallow="true" (GET)`

TODO

#### JSON Request Body:

```json
```

## `/.json?orderBy="{character}"{findCharacter} &limitToLast="{retrieval_limit}&timeout="{timeout_limit}"` (GET)

TODO

#### JSON Request Body:

```json
```
