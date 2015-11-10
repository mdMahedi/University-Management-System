
    $(document).ready(function () {
        var departmentDDL = $("#DepartmentId");
        var semisterDDL = $("#SemisterId");
        var courseDDL = $("#CourseId");
        var teacherDDL = $("#TeacherId");
        semisterDDL.prop("disabled", true);
        courseDDL.prop("disabled", true);
        teacherDDL.prop("disabled", true);
        departmentDDL.empty();
        semisterDDL.empty();
        courseDDL.empty();
        teacherDDL.empty();
        semisterDDL.append($("<option/>", { value: "", text: "Semister Locked" }));
        courseDDL.append($("<option/>", { value: "", text: "Course Locked" }));
        teacherDDL.append($("<option/>", { value: "", text: "Teacher Locked" }));
        $.ajax({
            url: 'AssiginCourseToTeacher/GetAllDepartment',
            method: 'post',
            dataType: 'json',
            success: function(data) {
                departmentDDL.append($("<option/>", { value: "", text: "Select Department" }));
                $(data).each(function(index, item) {
                    departmentDDL.append($("<option/>", { value: item.Value, text: item.Text }));
                });
            }
        });
        departmentDDL.change(function() {
            if ($(this).val() === "") {
                semisterDDL.empty();
                courseDDL.empty();
                teacherDDL.empty();
                semisterDDL.append($("<option/>", { value: "", text: "Semister Locked" }));
                courseDDL.append($("<option/>", { value: "", text: "Course Locked" }));
                teacherDDL.append($("<option/>", { value: "", text: "Teacher Locked" }));
                semisterDDL.prop("disabled", true);
                courseDDL.prop("disabled", true);
                teacherDDL.prop("disabled", true);
            } else {
                $.ajax({
                    url: 'AssiginCourseToTeacher/"GetSemister',
                    method: 'post',
                    dataType: 'json',
                    success: function (data) {
                        courseDDL.empty();
                        teacherDDL.empty();
                        semisterDDL.empty();
                        semisterDDL.append($("<option/>", { value: "", text: "Select Semister" }));
                        courseDDL.append($("<option/>", { value: "", text: "Course Locked" }));
                        teacherDDL.append($("<option/>", { value: "", text: "Teacher Locked" }));
                        courseDDL.prop("disabled", true);
                        teacherDDL.prop("disabled", true);
                        semisterDDL.prop("disabled", false);
                        $(data).each(function(i, item) {
                            semisterDDL.append($("<option/>", { value: item.Value, text: item.Text }));
                        });
                    }
                });
            }
        });
        semisterDDL.change(function() {
            if ($(this).val() === "") {
                courseDDL.empty();
                teacherDDL.empty();
                courseDDL.prop("disabled", true);
                teacherDDL.prop("disabled", true);
                courseDDL.append($("<option/>", { value: "", text: "Course Locked" }));
                teacherDDL.append($("<option/>", { value: "", text: "Teacher Locked" }));
            } else {
                $.ajax({
                    url: 'AssiginCourseToTeacher/GetCourseByDepartmentIdandSemisterId',
                    method: 'post',
                    data: { DepartmentId: departmentDDL.val(), SemisterId: $(this).val() },
                    dataType: 'json',
                    success: function(data) {
                        teacherDDL.prop("disabled", true);
                        courseDDL.prop("disabled", false);
                        teacherDDL.empty();
                        courseDDL.empty();
                        teacherDDL.append($("<option/>", { value: "", text: "Teacher  Locked" }));
                        courseDDL.append($("<option/>", { value: "", text: "Select Course" }));
                        $(data).each(function(i, item) {
                            courseDDL.append($("<option/>", { value: item.Value, text: item.Text }));
                        });
                    }
                });
            }
        });

        courseDDL.change(function() {
            if ($(this).val() === "") {
                teacherDDL.empty();
                teacherDDL.prop("disabled", true);
                teacherDDL.append($("<option/>", { value: "", text: "Teacher Locked" }));

            } else {
                $.ajax({
                    url: 'AssiginCourseToTeacher/GetTeacherByAssiginId',
                    method: 'post',
                    data: { CourseId: $(this).val() },
                    dataType: 'json',
                    success: function (data) {
                        if (data === "null") {
                            teacherDDL.prop("disabled", false);
                            teacherDDL.empty();
                            teacherDDL.append($("<option/>", { value: "", text: "Course Already assigined" }));
                        } else {
                            teacherDDL.prop("disabled", false);
                            teacherDDL.empty();
                            teacherDDL.append($("<option/>", { value: "", text: "Select Teacher" }));
                            $(data).each(function(i, item) {
                                teacherDDL.append($("<option/>", { value: item.Value, text: item.Text })); 
                            });
                        }
                    }});
            }
        });

    });
