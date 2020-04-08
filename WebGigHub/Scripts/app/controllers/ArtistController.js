class ArtistController {

    button;
    #followingService;
    
    constructor() {
        this.#followingService = new FollowingService();
    }

    follow = () => {
        $(".js-toggle-follow").click(this.#toggleFollow);
    };

    #toggleFollow = (e) => {
        this.button = $(e.target);
        let userId = this.button.attr("data-user-id");

        this.button.hasClass("btn-default") ?
            this.#followingService.addFollow(userId, this.#done, this.#fail)
            :
            this.#followingService.removeFollow(userId, this.#done, this.#fail)
    };

    #done = () => {
        let text = this.button.text() === "Following" ? "Following" : "Follow";
        this.button
            .toggleClass("btn-info")
            .toggleClass("btn-default")
            .text(text);
    };

    #fail = () => {
        alert("Something failed!")
    }
}

const artistController = new ArtistController();