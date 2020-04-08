class FollowingService {

    addFollow = (userId, done, fail) => {
        $.post("/api/followings", {followeeId: userId})
            .done(done)
            .fail(fail);
    };

    removeFollow = (userId, done, fail) => {
        $.ajax({
            url: "/api/followings/" + userId,
            method: "DELETE"
        })
            .done(done)
            .fail(fail);
    }

}