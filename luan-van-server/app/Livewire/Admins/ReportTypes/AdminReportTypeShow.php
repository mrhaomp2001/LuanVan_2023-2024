<?php

namespace App\Livewire\Admins\ReportTypes;

use App\Models\ReportType;
use Livewire\Component;

class AdminReportTypeShow extends Component
{
    public $report_type;
    public $name;
    public $description;
    public $model_type;
    public $can_use;
    public function mount($report_type_id)
    {
        $this->report_type = ReportType::findOrFail($report_type_id);
        $this->name = $this->report_type->name;
        $this->description = $this->report_type->description;
        $this->can_use = $this->report_type->can_use;
        $this->model_type = $this->report_type->model_type;
    }

    public function save()
    {
        $this->report_type->name = $this->name;
        $this->report_type->description = $this->description;
        $this->report_type->can_use = $this->can_use;

        $this->report_type->save();

        $this->redirect(route("admin.report-type.index"));
    }

    public function render()
    {
        return view('livewire.admins.report-types.admin-report-type-show');
    }
}
