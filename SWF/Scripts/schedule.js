$(function () {
    var date = $("#datepicker");
    date.val(moment().format("MM/DD/YYYY"));
    var btn = $("#btnGetSchedule");
    date.datepicker();

    btn.on("click", () => {
        //this.url += this.date.val();
        $.get(url, { date: date.val() })
            .done((schedule) => {
                //convert c# json return dateformat to js date
                schedule.forEach((eng) => {
                    eng.WorkDays.forEach((d, index) => {
                        eng.WorkDays[index] = moment(d);
                    });
                });

                var htmlSchedule = $("#schedule");
                htmlSchedule.html("");
                schedule.forEach((eng) => {
                    var li = $(document.createElement('li'));
                    li.html(eng.Name + " Works on: " + eng.WorkDays[0].format("MMM DD, YYYY") + ' and ' + eng.WorkDays[1].format("MMM DD, YYYY"));
                    htmlSchedule.append(li);
                });

                var range = GetWorkdayRange(schedule);
                var dateSchedule = ConvertScheduleByWorkdate(schedule);
                var htmlTimeSchedule = $("#timeSchedule");
                htmlTimeSchedule.html("");

                dateSchedule.forEach((s) => {
                    var li = $(document.createElement('li'));
                    li.html(s.date.format("MMM DD, YYYY") + ": ");
                    s.engineers.forEach((e) => {
                        li.append(e + ", ");
                    })
                    htmlTimeSchedule.append(li);
                });
               
            });
    });

    
});


var GetWorkdayRange =function (schedule) {
    var minDate = null;
    var maxDate = null;
    schedule.forEach((s) => {
        s.WorkDays.forEach((wd) => {
            //get mindate
            if (minDate == null) {
                minDate = wd
            }
            else if (wd < minDate) {
                minDate = wd;
            }
            //get max date
            if (maxDate == null) {
                maxDate = wd
            }
            else if (wd > maxDate) {
                maxDate = wd;
            }
        });
    });

    return {
        min: minDate,
        max: maxDate
    }
}


var ConvertScheduleByWorkdate = function (schedule) {
    var listDateSchedule = [];
    var range = GetWorkdayRange(schedule);

    for (var i = 0; i < range.max.diff(range.min, 'day'); i++) {
        var date = moment(range.min).add(i, 'day');
        var dateSchedule = {};
        dateSchedule.date = date;
        dateSchedule.engineers = [];

        schedule.forEach((s) => {
            s.WorkDays.forEach((wd) => {
                var dd = wd.diff(date, 'day');
                if (wd.diff(date, 'day') == 0) {
                    dateSchedule.engineers.push(s.Name);
                }
            });
        });

        listDateSchedule.push(dateSchedule);
    }

    return listDateSchedule;
}
