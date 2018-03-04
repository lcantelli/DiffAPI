Feature: Diff functionality should return inconsistencies between encoded JSONs

Scenario: Objects are the same

Given I POST a encoded json "eyJOYW1lIjoiUm9iZXJ0In0=" as "right" side under id "99"
And I POST a encoded json "eyJOYW1lIjoiUm9iZXJ0In0=" as "left" side under id "99"
When I GET the diffs from id "99"
Then I expect the response to be JSON:
"""
  {
  "id": "99",
  "message": "Objects are the same",
  "inconsistencies": []
  }
"""

Scenario: Objects are different

Given I POST a encoded json "eyJOYW1lIjoiUm9iZXJ0In0=" as "right" side under id "99"
And I POST a encoded json "eyJOYW1lIjoiTHVjYXMifQ==" as "left" side under id "99"
When I GET the diffs from id "99"
Then I expect the response to be JSON:
"""
  {
  "id": "99",
  "message": "Found 1 inconsistencies between jsons",
  "inconsistencies": [
    "Property 'Name' changed! From: Lucas - To: Robert"
  ]
  }
"""

Scenario: Objects are from different sizes

Given I POST a encoded json "eyJOYW1lIjoiUm9iZXJ0In0=" as "right" side under id "99"
And I POST a encoded json "eyJOYW1lIjoiTHVjYXMiLCAiTGFzdE5hbWUiOiAiQ2FudGVsbGkifQ==" as "left" side under id "99"
When I GET the diffs from id "99"
Then I expect the response to be JSON:
"""
  {
    "id":"99",
    "message":"Lenght of both JSONs doesn't match.",
    "inconsistencies":[]
  }
"""

Scenario: Not valid JSON format

When I POST a encoded json "ABCDEFGHIJ" as "right" side under id "99"
Then I expect the message to be "Failed to store JSON. Error Message: Invalid length for a Base-64 char array or string."