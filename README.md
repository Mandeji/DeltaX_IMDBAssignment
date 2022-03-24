# DeltaX_IMDBAssignment
I have Created this repository for Interview Process in 2 Days

Clone the Project and Run it to see the Endpoint for this API.

First Run the SQL file to create The Database .
Please Restore the Nuget Packages before building the Project {if haven't}.

Here Are the Endpoints and Request Body Format for calling this API.

**List Movie **:        /api/Movie/ListMovies
**Get Movie **:         /api/Movie/GetMovie/{id} -- Replace the ID 
**Add Movie ** :         /api/Movie/AddMovie 

**Movie Input Format** : 
{
  "id": 0,
  "movieName": "string",
  "dateRelease": "2022-03-24T12:40:51.260Z",
  "description": "string",
  "posterPath": "string",
  "producer": {
    "id": 0,
    "producerName": "string",
    "dateBirth": "2022-03-24T12:40:51.260Z",
    "companyName": "string",
    "bio": "string",
    "gender": "string"
  },
  "actors": [
    {
      "id": 0,
      "actorName": "string",
      "dateBirth": "2022-03-24T12:40:51.260Z",
      "bio": "string",
      "gender": "string"
    }
  ]
}


**UpdateMovie** :     /api/Movie/UpdateMovie

**Format For Updating Movie** :

{
  "id": 0,
  "movieName": "string",
  "dateRelease": "2022-03-24T12:44:22.400Z",
  "description": "string",
  "posterPath": "string",
  "producer": {
    "id": 0,
    "producerName": "string",
    "dateBirth": "2022-03-24T12:44:22.400Z",
    "companyName": "string",
    "bio": "string",
    "gender": "string"
  },
  "actors": [
    {
      "id": 0,
      "actorName": "string",
      "dateBirth": "2022-03-24T12:44:22.400Z",
      "bio": "string",
      "gender": "string"
    }
  ]
}


**DeleteMovie** :       /api/Movie/{id}  -- Replace ID

**Here is the Extra Endpoints to Add/List "Actor" and "Producer"**

List Producer :     /api/Producer/ListProducer
Add Producer :      /api/Producer/AddProducer


List Actor :        /api/Actor/ListActor
Add Actor  :        /api/Actor/AddActor
