<?php

namespace App\Livewire\Admins\Reports;

use App\Models\Report;
use App\Models\ReportRespone;
use Livewire\Component;

class AdminReportShow extends Component
{
    public $report;
    public $reponses;
    public $reponse_id;
    public $message;
    public $is_ban;
    public function mount($report_id)
    {
        $this->report = Report::find($report_id);
        $this->reponses = ReportRespone::all();
        $this->reponse_id = 1;
    }

    public function action()
    {
        if ($this->reponse_id == 1) {
            $this->message = "Hãy chọn một cách xử lý.";
            return;
        }

        $this->message = "";

        if ($this->report->model_type == "post") {
            if ($this->reponse_id == 3) {
                $this->report->model->post_status_id = 2;
            }
        }
        if ($this->report->model_type == "comment") {
            if ($this->reponse_id == 3) {
                $this->report->model->comment_status_id = 2;
            }
        }
        if ($this->report->model_type == "topic") {
            if ($this->reponse_id == 3) {
                $this->report->model->topic_status_id = 2;
            }
        }
        if ($this->report->model_type == "topic_comment") {
            if ($this->reponse_id == 3) {
                $this->report->model->topic_comment_status_id = 2;
            }
        }

        $this->report->responder = auth()->user()->id;
        $this->report->report_response_id = $this->reponse_id;
        $this->report->save();
        $this->report->model->save();

        if ($this->is_ban) {
            $this->report->model->user->is_ban = $this->is_ban;
            $this->report->model->user->save();
        }

        $this->redirect(route("admin.report.index"));
    }

    public function render()
    {
        return view('livewire.admins.reports.admin-report-show');
    }
}
