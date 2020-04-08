class AttendanceService {
    createAttendance = (gigId, done, fail) => {
        $.post("/api/attendances", {gigId})
            .done(done)
            .fail(fail);
    };

    deleteAttendances = (gigId, done, fail) => {
        $.ajax({
            url: "/api/attendances/" + gigId,
            method: "DELETE"
        })
            .done(done)
            .fail(fail);
    };
}