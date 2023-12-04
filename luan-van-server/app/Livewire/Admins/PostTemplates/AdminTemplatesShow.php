<?php

namespace App\Livewire\Admins\PostTemplates;

use App\Models\PostTemplate;
use Livewire\Component;

class AdminTemplatesShow extends Component
{
    public $template;
    public $name;
    public $content;
    public $theme_color;
    public $is_require_title;
    public $is_require_image;
    public $can_use;

    public function mount($template_id) {
        $this->template = PostTemplate::findorFail($template_id);

        $this->name = $this->template->name;
        $this->content = $this->template->content;
        $this->theme_color = $this->template->theme_color;
        $this->is_require_title = $this->template->is_require_title;
        $this->is_require_image = $this->template->is_require_image;
        $this->can_use = $this->template->can_use;
    }

    public function save() {
        $this->validate(
            [
                'name' => ["required", "min:3", 'max:64'],
                'content' => ["required", "min:3", 'max:512'],
            ],
            [
                'name.required' => "Cần nhập tên",
                'name.min' => "Cần nhập tên với tối thiểu :min ký tự",
                'name.max' => "Cần nhập tên với tối đa :max ký tự",

                'content.required' => "Cần nhập miêu  tả",
                'content.min' => "Cần nhập miêu tả với tối thiểu :min ký tự",
                'content.max' => "Cần nhập miêu tả với tối đa :max ký tự",

            ]
        );
        $this->template->name = $this->name;
        $this->template->content = $this->content;
        $this->template->theme_color = $this->theme_color;
        $this->template->is_require_title = $this->is_require_title;
        $this->template->is_require_image = $this->is_require_image;
        $this->template->can_use = $this->can_use;
        
        $this->template->save();

        $this->redirect(route("admin.template.index"));
    }

    public function render()
    {
        return view('livewire.admins.post-templates.admin-templates-show');
    }
}
