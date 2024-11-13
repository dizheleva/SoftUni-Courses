function manageMeetings(arr) {
    let meetings = [];

    for (const element of arr) {
        const [day, name] = element.split(' ');
        
        if (meetings.some(x => x.day === day)) {
            console.log(`Conflict on ${day}!`);
        } else {
            const meeting = {
                day: day,
                name: name
            };
            meetings.push(meeting);
            console.log(`Scheduled for ${meeting.day}`);
        }
    }

    for (const meeting of meetings) {
        console.log(`${meeting.day} -> ${meeting.name}`);
    }
}

manageMeetings(
    [
        'Monday Peter',
        'Wednesday Bill',
        'Monday Tim',
        'Friday Tim'
    ]
);
manageMeetings(
    [
        'Friday Bob',
        'Saturday Ted',
        'Monday Bill',
        'Monday John',
        'Wednesday George'
    ]
);