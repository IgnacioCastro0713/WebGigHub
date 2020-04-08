class GigsController {

    button;
    #attendanceService;

    constructor() {
        this.#attendanceService = new AttendanceService();
    }

    attendance = (container) => {
        $(container).on("click", ".js-toggle-attendance", this.#toggleAttendances);
    };

    #toggleAttendances = (e) => {
        this.button = $(e.target);
        let gigId = this.button.attr("data-gig-id");

        this.button.hasClass("btn-default") ?
            this.#attendanceService.createAttendance(gigId, this.#done, this.#fail)
            :
            this.#attendanceService.deleteAttendances(gigId, this.#done, this.#fail)
    };

    #done = () => {
        let text = (this.button.text() === "Going") ? "Going?" : "Going";
        this.button
            .toggleClass("btn-info")
            .toggleClass("btn-default")
            .text(text);
    };

    #fail = () => {
        alert("Something failed!");
    };
}

const gigsController = new GigsController();
