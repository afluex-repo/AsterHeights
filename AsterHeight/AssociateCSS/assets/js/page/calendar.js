let calendar;
var Draggable = FullCalendarInteraction.Draggable;
let date_picker;
let filter_option = "all";

var containerEl = document.getElementById("external-events");
var checkbox = document.getElementById("drop-remove");
var addEvent = document.getElementById("add-event");
var editEvent = document.getElementById("edit-event");
var addEventTitle = document.getElementById("addEventTitle");
var editEventTitle = document.getElementById("editEventTitle");

var date = new Date();
var day = date.getDate();
var month = date.getMonth();
var year = date.getFullYear();

(this.$eventModal = $("#event-modal")),
new Draggable(containerEl, {
  itemSelector: ".fc-event",
  eventData: function (eventEl) {
    return {
      title: eventEl.innerText,
      stick: true,
      className: eventEl.dataset.class,
    };
  },
});

$(document).ready(function () {
  initCalendar();
  addEvetClick();
  refresh_calendar_filter();
});

function initCalendar() {
  var calendarEl = $("#calendar").get(0);
  calendar = new FullCalendar.Calendar(calendarEl, {
    plugins: ["interaction", "dayGrid", "timeGrid"],
    header: {
      left: "prev,next today",
      center: "title",
      right: "dayGridMonth,timeGridWeek,timeGridDay",
    },
    editable: true,
    droppable: true,
    navLinks: true,
    eventLimit: true,
    weekNumberCalculation: "ISO",
    displayEventEnd: true,
    lazyFetching: true,
    selectable: true,
    eventMouseEnter: function (info) {
      $(info.el).attr('id', info.event.id);

      $('#' + info.event.id).popover({
        template: '<div class="popover" role="tooltip"><div class="arrow"></div><h3 class="popover-header"></h3><div class="popover-body"></div></div>',
        title: info.event.title,
        content: info.event.extendedProps.description,
        placement: 'top',
        html: true
      });
      $('#' + info.event.id).popover('show');
      $('.popover .popover-header').css('color', $(info.el).css('background-color'));
    },
    eventMouseLeave: function (info) {
      $('#' + info.event.id).popover('hide');
    },
    drop: function (info) {
      if (checkbox.checked) {
        info.draggedEl.parentNode.removeChild(info.draggedEl);
      }
    },
    views: {
      dayGridMonth: {
        eventLimit: 3,
      },
    },

    events: events(),

    eventRender: function (info) {
      if (filter_option !== "all" && info.event.groupId !== filter_option) {
        return false;
      }
    },
    select: function (start, end) {
      addEvent.style.display = "block";
      editEvent.style.display = "none";
      addEventTitle.style.display = "block";
      editEventTitle.style.display = "none";

      clearModalForm();
      $(".modal").modal("show");
    },
    eventClick: function (info) {
      addEvent.style.display = "none";
      editEvent.style.display = "block";
      addEventTitle.style.display = "none";
      editEventTitle.style.display = "block";

      let startDate = moment(info.event.start).format("YYYY-MM-DD HH:mm:ss");
      let endDate = moment(info.event.end).format("YYYY-MM-DD HH:mm:ss");

      console.log(info.event.extendedProps.description);
      $(".modal").modal("show");
      $(".modal").find("#title").val(info.event.title);
      $(".modal").find("#starts-at").val(startDate);
      $(".modal").find("#ends-at").val(endDate);
      $("#categorySelect").val(info.event.classNames[0]);
      $(".modal")
        .find("#eventDetails")
        .val(info.event.extendedProps.description);

      $("#edit-event")
        .off("click")
        .on("click", function (event) {
          event.preventDefault();
          var category = $("#categorySelect").find(":selected").val();

          var event = calendar.getEventById(info.event.id);
          event.setProp("title", document.getElementById("title").value);
          event.setStart(
            moment(document.getElementById("starts-at").value).format(
              "YYYY-MM-DD HH:mm:ss"
            )
          );
          event.setEnd(
            moment(document.getElementById("ends-at").value).format(
              "YYYY-MM-DD HH:mm:ss"
            )
          );
          event.setProp("classNames", [category]);
          $(".modal").modal("hide");
        });
    },
  });

  calendar.render();
}

function refresh_calendar_filter() {
  $(document).on("click", ".filter-container .filter", function (e) {
    filter_option = $(this).val();
    calendar.rerenderEvents();
  });
}

function clearModalForm() {
  var input = document.querySelectorAll('input[type="text"]');
  var textarea = document.getElementsByTagName("textarea");
  for (i = 0; i < input.length; i++) {
    input[i].value = "";
  }
  for (j = 0; j < textarea.length; j++) {
    textarea[j].value = "";
    i;
  }
}

function addEvetClick() {
  $("#add-event").on("click", function (event) {
    var title = $("#title").val();
    var eventDetails = document.getElementById("eventDetails").value;
    var category = $("#categorySelect").find(":selected").val();
    var groupId = $("#categorySelect").find(":selected").attr("id");
    var randomID = randomIDGenerate(
      10,
      "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"
    );
    calendar.addEvent({
      id: randomID,
      title: title,
      start: $("#starts-at").val(),
      end: $("#ends-at").val(),
      className: category,
      groupId: groupId,
      description: eventDetails,
    });
    // Clear modal inputs
    $(".modal").find("input").val("");
    // hide modal
    $(".modal").modal("hide");
  });
}

function randomIDGenerate(length, chars) {
  var result = "";
  for (var i = length; i > 0; --i)
    result += chars[Math.round(Math.random() * (chars.length - 1))];
  return result;
}

function events() {
  return [{
      id: "event1",
      title: "All Day Event",
      start: new Date(year, month, 1, 0, 0),
      end: new Date(year, month, 1, 23, 59),
      className: "fc-event-success",
      groupId: "work",
      description: "Her extensive perceived may any sincerity extremity. Indeed add rather may pretty see.",
    },
    {
      id: "event2",
      title: "Break",
      start: new Date(year, month, day + 28, 16, 0),
      end: new Date(year, month, day + 29, 20, 0),
      allDay: false,
      className: "fc-event-primary",
      groupId: "important",
      description: "Her extensive perceived may any sincerity extremity. Indeed add rather may pretty see. ",
    },
    {
      id: "event3",
      title: "Shopping",
      start: new Date(year, month, day + 4, 12, 0),
      end: new Date(year, month, day + 4, 20, 0),
      allDay: false,
      className: "fc-event-warning",
      groupId: "personal",
      description: "Her extensive perceived may any sincerity extremity. Indeed add rather may pretty see. ",
    },
    {
      id: "event4",
      title: "Meeting",
      start: new Date(year, month, day + 14, 10, 30),
      end: new Date(year, month, day + 16, 20, 0),
      allDay: false,
      className: "fc-event-success",
      groupId: "work",
      description: "Her extensive perceived may any sincerity extremity. Indeed add rather may pretty see.",
    },
    {
      id: "event5",
      title: "Lunch",
      start: new Date(year, month, day, 11, 0),
      end: new Date(year, month, day, 14, 0),
      allDay: false,
      className: "fc-event-primary",
      groupId: "important",
      description: "Her extensive perceived may any sincerity extremity. Indeed add rather may pretty see.",
    },
    {
      id: "event6",
      title: "Meeting",
      start: new Date(year, month, day + 2, 12, 30),
      end: new Date(year, month, day + 2, 14, 30),
      allDay: false,
      className: "fc-event-success",
      groupId: "work",
      description: "Her extensive perceived may any sincerity extremity. Indeed add rather may pretty see.",
    },
    {
      id: "event7",
      title: "Birthday Party",
      start: new Date(year, month, day + 17, 19, 0),
      end: new Date(year, month, day + 17, 19, 30),
      allDay: false,
      className: "fc-event-warning",
      groupId: "personal",
      description: "Her extensive perceived may any sincerity extremity. Indeed add rather may pretty see.",
    },
    {
      id: "event8",
      title: "Go to Delhi",
      start: new Date(year, month, day + -5, 10, 0),
      end: new Date(year, month, day + -4, 10, 30),
      allDay: false,
      className: "fc-event-danger",
      groupId: "travel",
      description: "Her extensive perceived may any sincerity extremity. Indeed add rather may pretty see.",
    },
    {
      id: "event9",
      title: "Get To Gather",
      start: new Date(year, month, day + 6, 10, 0),
      end: new Date(year, month, day + 7, 10, 30),
      allDay: false,
      className: "fc-event-info",
      groupId: "friends",
      description: "Her extensive perceived may any sincerity extremity. Indeed add rather may pretty see.",
    },
    {
      id: "event10",
      title: "Collage Party",
      start: new Date(year, month, day + 20, 10, 0),
      end: new Date(year, month, day + 20, 10, 30),
      allDay: false,
      className: "fc-event-info",
      groupId: "friends",
      description: "Her extensive perceived may any sincerity extremity. Indeed add rather may pretty see.",
    },
  ];
}