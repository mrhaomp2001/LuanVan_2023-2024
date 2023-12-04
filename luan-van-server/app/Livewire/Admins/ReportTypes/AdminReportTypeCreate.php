<?php

namespace App\Livewire\Admins\ReportTypes;

use App\Models\ReportType;
use Livewire\Component;

class AdminReportTypeCreate extends Component
{
    public $report_type;
    public $name;
    public $description;
    public $model_type;
    public $can_use;

    public function mount($model_type)
    {
        $this->model_type = $model_type;

        $this->can_use = false;
    }

    public function save()
    {
        $this->validate(
            [
                'name' => ["required", "min:3", 'max:64'],
                'description' => ["required", "min:3", 'max:512'],
            ],
            [
                'name.required' => "Cần nhập tên",
                'name.min' => "Cần nhập tên với tối thiểu :min ký tự",
                'name.max' => "Cần nhập tên với tối đa :max ký tự",

                'description.required' => "Cần nhập miêu tả",
                'description.min' => "Cần nhập miêu tả với tối thiểu :min ký tự",
                'description.max' => "Cần nhập miêu tả với tối đa :max ký tự",

            ]
        );
        $this->report_type = new ReportType();

        $this->report_type->name = $this->name;
        $this->report_type->description = $this->description;
        $this->report_type->can_use = $this->can_use;
        $this->report_type->model_type = $this->model_type;
        $this->report_type->save();

        $this->redirect(route("admin.report-type.index"));
    }

    public function render()
    {
        return view('livewire.admins.report-types.admin-report-type-create');
    }
}
