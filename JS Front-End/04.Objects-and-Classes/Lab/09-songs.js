function printSongs(arr) {
    class Song {
        constructor(typeList, name, time) {
            this.typeList = typeList;
            this.name = name;
            this.time = time;
        }
    }

    let songs = [];

    const songsCount = arr.shift();
    const desiredList = arr.pop();
    
    for (let i = 0; i < songsCount; i++) {
        const [typeList, name, time] = arr[i].split('_');
        songs.push(new Song(typeList, name, time));
    }
    
    if (desiredList !== 'all') {
        songs = songs.filter(s => s.typeList === desiredList);
    }

    songs.forEach(s => console.log(s.name));
}

printSongs(
    [
        3,
        'favourite_DownTown_3:14',
        'favourite_Kiss_4:16',
        'favourite_Smooth Criminal_4:01',
        'favourite'
    ]
);
printSongs(
    [
        4,
        'favourite_DownTown_3:14',
        'listenLater_Andalouse_3:24',
        'favourite_In To The Night_3:58',
        'favourite_Live It Up_3:48',
        'listenLater'
    ]
);
printSongs(
    [
        2,
        'like_Replay_3:15',
        'ban_Photoshop_3:48',
        'all'
    ]
);